using ApiDotNet.Application.DTOs;
using ApiDotNet.Application.Services;
using ApiDotNet.Application.Services.Interfaces;
using ApiDotNet.Domain.Entities;
using ApiDotNet.Domain.Repositories;
using Moq;
using Moq.AutoMock;

namespace ApiDotNet.UnitTest.Services
{
    public class PurchaseServiceTest
    {
        //No automock não precisamos passar explicitamente o nosso serviço(dar new Service e passar dependências, ele vai gerenciar isso automaticamente)
        //Pacote NuGet e instalar o pacote Moq.AutoMock

        [Fact(DisplayName = "Deve criar uma compra")]
        public async Task Salvar_CreateAsync_DeveSalvarUmaCompra()
        {
            //criar DTO
            var dto = new PurchaseDTO
            {
                CodErp = "321",
                Document = "123456",
                Price = 10,
                ProductName = "Teste"
            };

            //para salvar precisamos criar nosso automock
            var mocker = new AutoMocker();
            //vamos centralizar todas as ações nele, toda vez que formos setar algum retorno ou passar algum valor pra algum método vamos usar esse mock
            mocker.GetMock<IProductRepository>().Setup(x => x.GetIdByCodErpAsync("321")).ReturnsAsync(1);
            mocker.GetMock<IPersonRepository>().Setup(x => x.GetIdByDocumentAsync("123456")).ReturnsAsync(1);
            mocker.GetMock<IPurchaseRepository>().Setup(x => x.CreateAsync(It.IsAny<Purchase>())).ReturnsAsync(new Purchase(1, 1));

            var purchaseService = mocker.CreateInstance<PurchaseService>();
            //a linha acima equivale ao que fazemos em _personService = new PersonService(_personRepository.Object, new BaseTest().GetMapper());
            //mas no exemplo precisamos passar todas as dependências e com o automock ele gerencia pra gente

            //vamos validar nossos testes
            var result = await purchaseService.CreateAsync(dto);
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            //vamos validar se realmente nosso teste passou pelo create
            //Dentro de verify passar o método que queremos verificar e no final quantas vezes passou pelo método
            mocker.GetMock<IPurchaseRepository>().Verify(x => x.CreateAsync(It.IsAny<Purchase>()), Times.Once);
        }

        [Fact(DisplayName = "Deve deletar uma compra")]
        public async Task Deletar_DeleteAsync_DeveDeletarUmaCompra()
        {
            //primeiro precisamos carregar as compras
            var mocker = new AutoMocker();
            mocker.GetMock<IPurchaseRepository>().Setup(x => x.GetByIdAsync(1)).ReturnsAsync(new Purchase(1, 1));

            //vamos validar o teste
            var purchaseService = mocker.CreateInstance<PurchaseService>();
            var result = await purchaseService.DeleteAsync(1);
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);

            mocker.GetMock<IPurchaseRepository>().Verify(x => x.DeleteAsync(It.IsAny<Purchase>()), Times.Once);
        }
    }
}
