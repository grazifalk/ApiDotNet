using ApiDotNet.Application.DTOs;
using ApiDotNet.Application.Services;
using ApiDotNet.Domain.Entities;
using ApiDotNet.Domain.Repositories;
using Moq;

namespace ApiDotNet.UnitTest.Services
{
    public class ProductServiceTest
    {
        private Mock<IProductRepository> _productRepository;
        private ProductService _productService;

        [Fact(DisplayName = "Deve criar um produto")]
        public async Task Salvar_CreateAsync_DeveCriarUmProduto()
        {
            var dto = new ProductDTO
            {
                Name = "Coca-Cola",
                Id = 1,
                CodErp = "123456",
                Price = 14
            };

            //criar mock
            _productRepository = new Mock<IProductRepository>();
            //setar informações para Create
            _productRepository.Setup(x => x.CreateAsync(It.IsAny<Product>())).ReturnsAsync(It.IsAny<Product>());

            //instanciar nosso serviço
            _productService = new ProductService(_productRepository.Object, new BaseTest().GetMapper());
            var result = await _productService.CreateAsync(dto);
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);

            _productRepository.Verify(x => x.CreateAsync(It.IsAny<Product>()), Times.Once);
        }

        [Fact(DisplayName = "Não deve criar produto com objeto nulo")]
        public async Task NaoSalva_CreateAsync_NaoDeveCriarProdutoComObjetoNulo()
        {
            ProductDTO dto = null;

            //criar mock
            _productRepository = new Mock<IProductRepository>();

            //instanciar nosso serviço
            _productService = new ProductService(_productRepository.Object, new BaseTest().GetMapper());
            var result = await _productService.CreateAsync(dto);
            Assert.False(result.IsSuccess);
            Assert.Equal("Objeto deve ser informado!", result.Message);
        }

        [Fact(DisplayName = "Deve editar um produto")]
        public async Task Edita_UpdateAsync_DeveEditarUmProduto()
        {
            var dto = new ProductDTO
            {
                Name = "Coca-Cola",
                Id = 1,
                CodErp = "123456",
                Price = 14
            };

            //criar mock
            _productRepository = new Mock<IProductRepository>();
            var product = new Product("Agua", "5464", 4);
            //primeiro retornarmos algum item, para depois editar
            _productRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(product);

            //mockar/setar valor para o Edit
            _productRepository.Setup(x => x.UpdateAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);

            //instanciar nosso serviço
            _productService = new ProductService(_productRepository.Object, new BaseTest().GetMapper());

            var result = await _productService.UpdateAsync(dto);
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);

            _productRepository.Verify(x => x.UpdateAsync(It.IsAny<Product>()), Times.Once);

            Assert.Equal("Coca-Cola", product.Name);
        }
        
    }
}
