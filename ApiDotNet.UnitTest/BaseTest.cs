using ApiDotNet.Application.Mappings;
using AutoMapper;

namespace ApiDotNet.UnitTest
{
    public class BaseTest
    {
        public IMapper GetMapper()
        {
            var config = new MapperConfiguration(op =>
            {
                //vamos passar as duas classes que usamos pra fazer o mapper, DomainToDTO e DTOToDomain
                op.AddProfile<DomainToDtoMapping>();
                op.AddProfile<DtoToDomainMapping>();
            });

            return config.CreateMapper();
        }
    }
}
