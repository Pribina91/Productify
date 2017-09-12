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
using Meninx.Productify.Service.Configuration;
using Meninx.Productify.Service.Contracts;
using Newtonsoft.Json;

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
                var dbProduct = productRepository.GetById(product.Id);

                productRepository.Update(Mapper.Map<ProductContract, Product>(product, dbProduct));
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
                var attributeTypeContracts = attributeTypeRepository.Table.ProjectTo<AttributeTypeContract>().ToList();

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
    }
}