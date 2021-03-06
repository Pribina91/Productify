﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Meninx.Productify.Data.Models;
using Meninx.Productify.Service.Contracts;

namespace Meninx.Productify.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IProductifyService
    {
        [OperationContract]
        List<ProductContract> GetData(string productName, string code, int? price);

        [OperationContract]
        void AddProduct(ProductContract product);

        [OperationContract]
        void UpdateProduct(ProductContract product);

        [OperationContract]
        void RemoveProduct(int productId);

        [OperationContract]
        List<AttributeContract> GetProductAttributes(int productId);

        [OperationContract]
        FileInfo GetXml(string productName, string code, int? price);

        [OperationContract]
        FileInfo GetJson(string productName, string code, int? price);

        [OperationContract]
        List<AttributeTypeContract> GetAttributeTypes();
    }
}