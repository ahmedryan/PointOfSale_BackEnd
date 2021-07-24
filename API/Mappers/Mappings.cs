using API.Dtos;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace API.Mappers
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(x => x.Category, 
                    y => y
                        .MapFrom(s => s.Category.Name))
                .ForMember(x => x.Color, 
                    y => y
                        .MapFrom(s => s.Color.Name))
                .ForMember(x => x.Size, 
                    y => y
                        .MapFrom(s => s.Size.Name))
                .ForMember(x => x.Fit, 
                    y => y
                        .MapFrom(s => s.Fit.Name))
                .ReverseMap();
            
            CreateMap<IdentityUser, RegistrationDto>().ReverseMap();
            CreateMap<Product, CreateProductDto>().ReverseMap();
            CreateMap<Product, EditProductDto>().ReverseMap();
            CreateMap<Customer, CreateCustomerDto>().ReverseMap();
            CreateMap<Sale, CreateSaleDto>().ReverseMap();
        }
    }
}
