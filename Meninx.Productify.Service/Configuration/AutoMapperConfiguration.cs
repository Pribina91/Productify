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
                    cfg.CreateMap<Meninx.Productify.Data.Models.AttributeType, AttributeTypeContract>()
                        .ForMember(d => d.Name, s => s.MapFrom(sm => sm.Name))
                        .ForMember(d => d.DataTypeId, s => s.MapFrom(sm => (int)sm.DataType))
                        .ForMember(d => d.DataTypeName, s => s.MapFrom(sm => Enum.GetName(sm.DataType.GetType(), sm.DataType)));

                    cfg.CreateMap<Meninx.Productify.Data.Models.Attribute, AttributeContract>()
                        .ForMember(d => d.AttributeTypeId, s => s.MapFrom(sm => sm.AttributeTypeId))
                        .ForMember(d => d.AttributeTypeName, s => s.MapFrom(sm => sm.AttributeType.Name))
                        .ForMember(d => d.Value, s => s.MapFrom(sm => sm.GetStringedValue()));
                    cfg.CreateMap<Product, ProductContract>()
                        .ForMember(
                            d => d.Attributes,
                            expression => expression.MapFrom(s => s.Attributes));
                    cfg.CreateMap<ProductContract, Product>();
                    //.ForMember(m => m.Attributes, o => o.UseDestinationValue());
                    //cfg.AddProfile<FooProfile>();
                });
        }
    }
}