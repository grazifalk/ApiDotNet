using ApiDotNet.Application.Mappings;
using ApiDotNet.Application.Services;
using ApiDotNet.Application.Services.Interfaces;
using ApiDotNet.Domain.Authentication;
using ApiDotNet.Domain.Repositories;
using ApiDotNet.Infra.Data.Authentication;
using ApiDotNet.Infra.Data.Context;
using ApiDotNet.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiDotNet.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //vamos injetar serviços que fazem parte da nossa infra estrutura (banco e repositórios)
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IPurchaseRepository, PurchaseRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }

        //injetar serviços
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(DomainToDtoMapping));
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IPurchaseService, PurchaseService>();
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
