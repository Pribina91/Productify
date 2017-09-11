using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Meninx.Productify.Data;
using Meninx.Productify.Data.Context;
using Meninx.Productify.Data.Models;

namespace Meninx.Productify.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class ProductifyService : IProductifyService, IDisposable
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<Product> productRepository;


        public ProductifyService()
        {
            productRepository = unitOfWork.Repository<Product>();
        }

        public IQueryable<Product> GetData(string productName, string attributeName)
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

                if (!string.IsNullOrEmpty(attributeName))
                {
                    query = query.Where(
                        q => q.Attributes != null
                             && q.Attributes.Any(a => a.GetStringedValue().ToLower().Trim().Contains(attributeName)));
                }

                return query;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void AddProduct(Product product)
        {
            try
            {
                productRepository.Insert(product);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void UpdateProduct(Product product)
        {
            try
            {
                productRepository.Update(product);
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