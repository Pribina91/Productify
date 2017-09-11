using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Meninx.Productify.Data.Models;

namespace Meninx.Productify.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IProductifyService
    {
        [OperationContract]
        IQueryable<Product> GetData(string productName, string attributeName);

        [OperationContract]
        void AddProduct(Product product);

        [OperationContract]
        void UpdateProduct(Product product);

        [OperationContract]
        void RemoveProduct(int productId);
    }
}