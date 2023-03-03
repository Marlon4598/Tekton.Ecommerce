using System;
using AutoMapper;
using Tekton.Ecommerce.Domain.Entity;
using Tekton.Ecommerce.Application.DTO;

namespace Tekton.Ecommerce.Transversal.Mapper
{
    public class MappingsProfile: Profile
    {
        public MappingsProfile()
        {
            CreateMap<Products, ProductsDto>().ReverseMap()
                .ForMember(destination => destination.ProductID, source => source.MapFrom(src => src.ProductID))
                .ForMember(destination => destination.Name, source => source.MapFrom(src => src.Name))
                .ForMember(destination => destination.Status, source => source.MapFrom(src => src.Status))
                .ForMember(destination => destination.Stock, source => source.MapFrom(src => src.Stock))
                .ForMember(destination => destination.Description, source => source.MapFrom(src => src.Description))
                .ForMember(destination => destination.Price, source => source.MapFrom(src => src.Price));
        }
    }
}