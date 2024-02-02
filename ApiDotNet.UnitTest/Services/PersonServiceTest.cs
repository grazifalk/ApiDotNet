using ApiDotNet.Application.DTOs;
using ApiDotNet.Application.Services;
using ApiDotNet.Domain.Entities;
using ApiDotNet.Domain.Repositories;
using ApiDotNet.Infra.Data.Repositories;
using Moq;

namespace ApiDotNet.UnitTest.Services
{
    public class PersonServiceTest
    {
        //instalar com NuGet pacote "Moq"

        private Mock<IPersonRepository> _personRepository;
        private PersonService _personService;

        [Fact(DisplayName = "Deve criar uma pessoa")]
        public async Task Salvar_CreateAsync_DeveSalvarPessoa()
        {
            var dto = new PersonDTO()
            {
                Document = "2133131",
                Name = "Nome Pessoa Teste",
                Phone = "999999999",
                Id = 1
            };

            //Criar objeto do Mock e estamos mockando o método, It.IsAny é qualquer valor que for do Person e retornar qualquer valor, não precisamos passar específico
            _personRepository = new Mock<IPersonRepository>();
            _personRepository.Setup(x => x.CreateAsync(It.IsAny<Person>())).ReturnsAsync(It.IsAny<Person>());

            _personService = new PersonService(_personRepository.Object, new BaseTest().GetMapper());
            var result = await _personService.CreateAsync(dto);
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);

            //vamos validar nosso repositório para verificar se realmente nosso teste passou por esse método
            _personRepository.Verify(x => x.CreateAsync(It.IsAny<Person>()), Times.Once);
        }

        //vamos validar nossas validações para verificar se estão funcionando
        [Fact(DisplayName = "Não deve criar pessoa sem objeto")]
        public async Task NaoCriaPessoa_CreateAsync_NaoDeveSalvarPessoaComObjetoNulo()
        {
            //DTO será nula
            PersonDTO dto = null;
            _personRepository = new Mock<IPersonRepository>();

            _personService = new PersonService(_personRepository.Object, new BaseTest().GetMapper());
            var result = await _personService.CreateAsync(dto);
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Equal("Objeto deve ser informado!", result.Message);
        }

    }

}
