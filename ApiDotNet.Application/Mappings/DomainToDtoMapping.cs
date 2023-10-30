using ApiDotNet.Application.DTOs;
using ApiDotNet.Domain.Entities;
using AutoMapper;

namespace ApiDotNet.Application.Mappings
{
    public class DomainToDtoMapping : Profile
    {
        public DomainToDtoMapping()
        {
            CreateMap<Person, PersonDTO>();
            CreateMap<Product, ProductDTO>();
            CreateMap<Purchase, PurchaseDetailsDTO>() //passar entidade e dto
                .ForMember(x => x.Person, opt => opt.Ignore()) //vamos precisar ignorar alguns campos, se não ignorarmos, na hora de mapear ao invés de pegar o nome pegará a entidade
                .ForMember(x => x.Product, opt => opt.Ignore()) //se não ignorar ele pega o objeto e não a informação (dado) do objeto
                .ConstructUsing((model, context) => //vamos customizar os nossos campos - o model é nossa entidade
                {
                    var dto = new PurchaseDetailsDTO
                    {
                        Product = model.Product.Name,
                        Id = model.Id,
                        Date = model.Date,
                        Person = model.Person.Name
                    };
                    return dto;
                });
        }
    }
}
