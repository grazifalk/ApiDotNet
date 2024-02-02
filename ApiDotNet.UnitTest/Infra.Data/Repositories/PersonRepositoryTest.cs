using ApiDotNet.Domain.Entities;
using ApiDotNet.Infra.Data.Context;
using ApiDotNet.Infra.Data.Repositories;
using ApiDotNet.UnitTest.Infra.Data.Context;

namespace ApiDotNet.UnitTest.Infra.Data.Repositories
{
    public class PersonRepositoryTest
    {
        //criar variável privada
        private ApplicationDbContext _context;

        [Fact(DisplayName ="Deve criar uma pessoa")]
        public async Task Salva_CreateAsync_DeveSalvarUmaPessoaNoBancoDeDados()
        {
            //criar o objeto de pessoa
            var person = new Person("5645646456", "Teste de unidade", "99999999");
            //chamar o banco de teste em memória
            _context = TesteDatabaseInMemory.GetDatabase();

            //criar o repositório passando dbContext do banco em memória e não original
            var personRepository = new PersonRepository(_context);
            await personRepository.CreateAsync(person);

            var result = _context.People.FirstOrDefault();
            Assert.Equal(person.Document, result.Document);
        }

        [Fact(DisplayName = "Deve retornar pessoa pelo seu ID")]
        public async Task ListaPessoa_GetByIdAsync_DeveRetornarAPessoaPeloSeuID()
        {
            //primeiro mockamos a pessoa dentro do nosso banco
            var person = new Person("5645646456", "Teste de unidade", "99999999");
            //chamar o banco de teste em memória
            _context = TesteDatabaseInMemory.GetDatabase();
            _context.People.Add(person);
            _context.SaveChanges();

            //criar o repositório passando dbContext do banco em memória e não original
            var personRepository = new PersonRepository(_context);
            //chamar o método informando o ID
            var result = await personRepository.GetByIdAsync(1);

            //depois testamos o retorno - vamos comparar
            Assert.Equal(person.Document, result.Document);
        }
    }
}
