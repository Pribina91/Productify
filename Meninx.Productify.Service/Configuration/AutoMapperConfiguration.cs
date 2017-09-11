using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using AutoMapper.Configuration;
using AutoMapper.Mappers;
using Meninx.Productify.Data.Models;
using Meninx.Productify.Service.Contracts;

namespace Meninx.Productify.Service.Configuration
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
          
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Meninx.Productify.Data.Models.Attribute, AttributeContract>();
                cfg.CreateMap<Product, ProductContract>();
                cfg.CreateMap<ProductContract, Product>();
                //.ForMember(m => m.Attributes, o => o.UseDestinationValue());
                //cfg.AddProfile<FooProfile>();
            });
        }
    }
}