using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Meninx.Productify.Core.Repositories;
using Meninx.Productify.Core.Tests.Fixtures;
using Meninx.Productify.Data.Context;
using Meninx.Productify.Data.Models;
using Meninx.Productify.Web.ProductifyServiceReference;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Attribute = Meninx.Productify.Data.Models.Attribute;
using AttributeType = Meninx.Productify.Data.Models.AttributeType;
using AttributeTypeDataType = Meninx.Productify.Data.Models.Enums.AttributeTypeDataType;
using Product = Meninx.Productify.Data.Models.Product;

namespace Meninx.Productify.Core.Tests
{
    [TestClass]
    public class ProductRepositoryTest
    {
        [TestMethod]
        public void Should_ReturnProducts()
        {
            var products = new FakeDbSet<Product>()
            {
                new Product()
                {
                    Code = "aaa",
                    Id = 1,
                    Name = "AAA",
                    Price = 11,
                }
            };

            var mockContext = new Mock<IProductifyContext>();
            mockContext.Setup(c => c.Products).Returns(products);

            var service = new ProductRepository(mockContext.Object);
            var prods = service.Table.ToList();

            Assert.AreEqual(1, prods.Count);
            Assert.AreEqual("aaa", prods[0].Code);
            Assert.AreEqual("AAA", prods[0].Name);
        }

        [TestMethod]
        public void Should_ReturnProductsWithAttributes()
        {
            var products = new FakeDbSet<Product>()
            {
                new Product()
                {
                    Code = "aaa",
                    Id = 1,
                    Name = "AAA",
                    Price = 11,
                    Attributes = new List<Attribute>()
                    {
                        new StringAtrribute()
                        {
                            Id = 3,
                            Value = "English",
                            AttributeType = new AttributeType()
                            {
                                Name = "Language",
                                DataType = AttributeTypeDataType.StringValue,
                                Id = 2,
                            }
                        }
                    }
                }
            };

            var mockContext = new Mock<IProductifyContext>();
            mockContext.Setup(c => c.Products).Returns(products);

            var service = new ProductRepository(mockContext.Object);
            var prods = service.Table.ToList();

            Assert.AreEqual(1, prods.Count);
            Assert.AreEqual("aaa", prods[0].Code);
            Assert.AreEqual("AAA", prods[0].Name);
        }
    }
}