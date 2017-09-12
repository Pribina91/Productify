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
            Mapper.Initialize(
                cfg =>
                {
                    cfg.CreateMap<Meninx.Productify.Data.Models.Attribute, AttributeContract>()
                        .ForMember(d => d.Value, s => s.MapFrom(sm => sm.GetStringedValue()));
                    cfg.CreateMap<Product, ProductContract>()
                        .ForMember(
                            d => d.Attributes,
                            expression => expression.MapFrom(
                                s => s.Attributes.Select(a => $"{a.AttributeType.Name}={a.GetStringedValue()}")));
                    cfg.CreateMap<ProductContract, Product>();
                    //.ForMember(m => m.Attributes, o => o.UseDestinationValue());
                    //cfg.AddProfile<FooProfile>();
                });
        }
    }
}