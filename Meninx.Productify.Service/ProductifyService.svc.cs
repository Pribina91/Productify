using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Meninx.Productify.Core;
using Meninx.Productify.Core.Repositories;
using Meninx.Productify.Data;
using Meninx.Productify.Data.Context;
using Meninx.Productify.Data.Models;
using Meninx.Productify.Data.Models.Enums;
using Meninx.Productify.Service.Configuration;
using Meninx.Productify.Service.Contracts;
using Meninx.Productify.Service.Exceptions;
using Newtonsoft.Json;
using Attribute = Meninx.Productify.Data.Models.Attribute;

namespace Meninx.Productify.Service
{
    public class ProductifyService : IProductifyService, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        private ProductRepository productRepository;
        private AttributeTypeRepository attributeTypeRepository;

        public ProductifyService()
        {
            productRepository = new ProductRepository(unitOfWork.GetContext());
            attributeTypeRepository = new AttributeTypeRepository(unitOfWork.GetContext());
            AutoMapperConfiguration.Configure();
        }

        public FileInfo GetJson(string productName, string code, int? price)
        {
            var data = GetData(productName, code, price);

            var serialized = JsonConvert.SerializeObject(data);
            FileInfo file = new FileInfo(Guid.NewGuid() + "json.txt");
            StreamWriter sw = file.AppendText();
            sw.Write(serialized);
            sw.Close();

            return file;
        }

        public FileInfo GetXml(string productName, string code, int? price)
        {
            var data = GetData(productName, code, price);

            var writer = new System.Xml.Serialization.XmlSerializer(typeof(List<ProductContract>));
            FileInfo file = new FileInfo(Guid.NewGuid() + ".xml");
            StreamWriter sw = file.AppendText();
            writer.Serialize(sw, data);
            sw.Close();

            return file;
        }

        public List<ProductContract> GetData(string productName, string code, int? price)
        {
            try
            {
                var query = productRepository.Table;

                if (!string.IsNullOrEmpty(productName))
                {
                    query = query.Where(
                        q => q.Name != null
                             && q.Name.ToLower().Trim().Contains(productName));
                }

                if (!string.IsNullOrEmpty(code))
                {
                    query = query.Where(
                        q => q.Code != null
                             && q.Code.ToLower().Trim().Contains(code));
                }

                return query.ToList().Select(Mapper.Map<Product, ProductContract>).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<AttributeContract> GetProductAttributes(int productId)
        {
            try
            {
                var product = productRepository.GetById(productId);
                var mapped = product.Attributes.ToList()
                    .Select(Mapper.Map<Meninx.Productify.Data.Models.Attribute, AttributeContract>).ToList();
                return mapped;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void AddProduct(ProductContract product)
        {
            try
            {
                ValidateProduct(product);

                productRepository.Insert(Mapper.Map<ProductContract, Product>(product));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void UpdateProduct(ProductContract product)
        {
            try
            {
                ValidateProduct(product);

                var dbProduct = productRepository.GetById(product.Id);
                //map attribute 
                dbProduct.Attributes.Clear();
                var attributes = new List<Attribute>();
                foreach (var attribute in product.Attributes)
                {
                    var dbType = attributeTypeRepository.GetById(attribute.AttributeTypeId);
                    switch (dbType.DataType)
                    {
                        case AttributeTypeDataType.StringValue:
                            dbProduct.Attributes.Add(Mapper.Map<AttributeContract, StringAtrribute>(attribute));
                            break;
                        case AttributeTypeDataType.IntegerValue:
                            dbProduct.Attributes.Add(Mapper.Map<AttributeContract, IntegerAtrribute>(attribute));
                            break;
                        case AttributeTypeDataType.DatetimeValue:
                            dbProduct.Attributes.Add(Mapper.Map<AttributeContract, DatetimeAtrribute>(attribute));
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                dbProduct = Mapper.Map<ProductContract, Product>(product, dbProduct);
                productRepository.Update(dbProduct);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        public void RemoveProduct(int productId)
        {
            try
            {
                var product = productRepository.GetById(productId);
                if (product == null)
                {
                    throw new ArgumentOutOfRangeException(nameof(productId));
                }

                productRepository.Delete(product);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<AttributeTypeContract> GetAttributeTypes()
        {
            try
            {
                var attributeTypeContracts = attributeTypeRepository.Table
                    .AsEnumerable()
                    .Select(Mapper.Map<AttributeType, AttributeTypeContract>)
                    .ToList();

                return attributeTypeContracts;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
            //base.Dispose(disposing);
        }


        private void ValidateProduct(ProductContract product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            if (string.IsNullOrEmpty(product.Name))
            {
                throw new ArgumentNullException(nameof(product.Name));
            }
            if (string.IsNullOrEmpty(product.Code))
            {
                throw new ArgumentNullException(nameof(product.Code));
            }

            if (product.Attributes == null)
            {
                throw new ArgumentNullException(nameof(product.Attributes));
            }

            foreach (var attribute in product.Attributes)
            {
                ValidateAttribute(attribute);
            }
        }

        private void ValidateAttribute(AttributeContract attribute)
        {
            if (attribute == null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            if (this.attributeTypeRepository.Table.Any(a => a.Id == attribute.AttributeTypeId))
            {
                throw new ArgumentOutOfRangeException(nameof(attribute.AttributeTypeId));
            }
        }
    }
}