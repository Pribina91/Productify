using System.Collections.Generic;
using Meninx.Productify.Data.Models;
using Meninx.Productify.Data.Models.Enums;

namespace Meninx.Productify.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Meninx.Productify.Data.Context.ProductifyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Meninx.Productify.Data.Context.ProductifyContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            var products = new List<Product>()
            {
                new Product()
                {
                    Id = 1,
                    Code = "Aa123",
                    Name = "AProduct",
                    Price = 295,
                },
            };
            products.ForEach(p => context.Products.Add(p));

            var attributeTypes = new List<AttributeType>()
            {
                new AttributeType()
                {
                    Id = 1,
                    Name = "Color",
                    DataType = AttributeTypeDataType.StringValue,
                },
                new AttributeType()
                {
                    Id = 2,
                    Name = "Descriptor",
                    DataType = AttributeTypeDataType.StringValue,
                },
                new AttributeType()
                {
                    Id = 3,
                    Name = "Length",
                    DataType = AttributeTypeDataType.IntegerValue,
                },
                new AttributeType()
                {
                    Id = 3,
                    Name = "ServiceDate",
                    DataType = AttributeTypeDataType.DatetimeValue,
                },
            };
            attributeTypes.ForEach(p => context.AttributeTypes.Add(p));

            var attributes = new List<Models.Attribute>()
            {
                new StringAtrribute()
                {
                    Id = 1,
                    Product = products,
                    Value = "Blue",
                    AttributeTypeId = 1,
                }
            };
            attributes.ForEach(p => context.Atrributes.Add(p));
            context.SaveChanges();
        }
    }
}