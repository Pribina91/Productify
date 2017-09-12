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
                    cfg.CreateMap<ProductContract, Product>()
                        .ForMember(d => d.Attributes, expression => expression.Ignore())
                        .ForSourceMember(s => s.Attributes, expression => expression.Ignore());
                    cfg.CreateMap<AttributeContract, StringAtrribute>()
                        .ForMember(d => d.Value, s => s.MapFrom(sm => sm.Value))
                        .ForMember(d => d.AttributeTypeId, s => s.MapFrom(sm => sm.AttributeTypeId))
                        .ForMember(d => d.Id, s => s.MapFrom(sm => sm.Id));
                    cfg.CreateMap<AttributeContract, DatetimeAtrribute>()
                        .ForMember(d => d.Value, s => s.MapFrom(sm => Convert.ToDateTime(sm.Value)))
                        .ForMember(d => d.AttributeTypeId, s => s.MapFrom(sm => sm.AttributeTypeId))
                        .ForMember(d => d.Id, s => s.MapFrom(sm => sm.Id));
                    cfg.CreateMap<AttributeContract, IntegerAtrribute>()
                        .ForMember(d => d.Value, s => s.MapFrom(sm => Convert.ToInt32(sm.Value)))
                        .ForMember(d => d.AttributeTypeId, s => s.MapFrom(sm => sm.AttributeTypeId))
                        .ForMember(d => d.Id, s => s.MapFrom(sm => sm.Id));
                    //cfg.CreateMap<AttributeContract,Meninx.Productify.Data.Models.Attribute>()
                    //cfg.
                    //.ForMember(d=>d.AttributeTypeId, sm => sm.MapFrom(s => s.AttributeTypeId))
                    //.ForMember(d=>d., sm => sm.MapFrom(s => s.AttributeTypeId))
                    //.ForMember()
                    //.ForMember(m => m.Attributes, o => o.UseDestinationValue());
                    //cfg.AddProfile<FooProfile>();
                });
        }
    }
}