using ApiDotNet.Application.DTOs;
using ApiDotNet.Domain.Entities;
using AutoMapper;

namespace ApiDotNet.Application.Mappings
{
    public class DtoToDomainMapping : Profile
    {
        public DtoToDomainMapping()
        {
            CreateMap<PersonDTO, Person>();
            CreateMap<ProductDTO, Product>();
        }
    }
}
