using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

namespace Meninx.Productify.Service
{
    public class ProductifyService : IProductifyService, IDisposable
    { 
        private UnitOfWork unitOfWork = new UnitOfWork();

        private ProductRepository productRepository;
        public ProductifyService()
        {
            productRepository = new ProductRepository(unitOfWork.GetContext());
            AutoMapperConfiguration.Configure();
        }

        public List<ProductContract> GetData(string productName, string code)
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

                return query.ToList().Select(Mapper.Map<Product,ProductContract>).ToList();
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
                var mapped = product.Attributes.ToList().Select(Mapper.Map<Meninx.Productify.Data.Models.Attribute, AttributeContract>).ToList();
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
                productRepository.Update(Mapper.Map<ProductContract, Product>(product));
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

        public void Dispose()
        {
            unitOfWork.Dispose();
            //base.Dispose(disposing);
        }
    }
}