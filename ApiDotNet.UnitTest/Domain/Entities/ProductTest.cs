using ApiDotNet.Domain.Entities;
using ApiDotNet.Domain.Validations;

namespace ApiDotNet.UnitTest.Domain.Entities
{
    public class ProductTest
    {
        [Fact(DisplayName ="Não deve criar um produto sem nome")]
        public void CriaProduto_Product_NaoCriaProdutoSemNome()
        {
            var ex = Assert.Throws<DomainValidationException>(
                        () => new Product(null, "445454", 20));

            Assert.Equal("Nome deve ser informado!", ex.Message);
        }

        [Fact(DisplayName = "Não deve criar um produto sem codErp")]
        public void CriaProduto_Product_NaoCriaProdutoSemCodigoErp()
        {
            var ex = Assert.Throws<DomainValidationException>(
                        () => new Product("Teste produto", null, 20));

            Assert.Equal("Código erp deve ser informado!", ex.Message);
        }

        [Fact(DisplayName = "Deve criar produto sem id")]
        public void CriaProduto_Product_DeveCriarProdutoSemId()
        {
            var product = new Product("Teste produto", "123456", 50);

            //se quisermos validar antes
            //Assert.Null(product);

            //testar se criou nosso objeto
            Assert.NotNull(product);            
        }

        [Fact(DisplayName = "Não deve criar um produto sem id")]
        public void CriaProduto_Product_NaoCriaProdutoSemId()
        {
            var ex = Assert.Throws<DomainValidationException>(
                        () => new Product(0, "Teste produto", "15651", 20));

            Assert.Equal("Id do produto deve ser informado!", ex.Message);
        }

        [Fact(DisplayName = "Deve criar produto com id")]
        public void CriaProduto_Product_DeveCriarProdutoComId()
        {
            var product = new Product(3, "Teste produto", "123456", 50);

            //testar se criou nosso objeto
            Assert.NotNull(product);
        }
    }
}
