using ApiDotNet.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ApiDotNet.UnitTest.Infra.Data.Context
{
    public class TesteDatabaseInMemory
    {
        //importar pacote para simular banco em memória
        //Microsoft.EntityFrameworkCore.InMemory

        //método privado
        private static ApplicationDbContext GetDatabase(string name)
        {
            var inMemoryOption = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(name).Options;

            return new ApplicationDbContext(inMemoryOption);
        }

        //método público
        public static ApplicationDbContext GetDatabase()
        {
            var name = Guid.NewGuid().ToString();
            return GetDatabase(name); //retornar método privado passando nosso nome
        }

        //Na hora de testar os repositórios, ao invés de chamar o ApplicationContext ou mockar, vamos chamar essa classe TesteDatabaseInMemory
    }
}
