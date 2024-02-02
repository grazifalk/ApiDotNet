using ApiDotNet.Domain.Entities;
using ApiDotNet.Domain.Validations;

namespace ApiDotNet.UnitTest.Domain.Entities
{
    public class PurchaseTest
    {
        [Fact(DisplayName ="Não deve criar compra sem o id do produto")]
        public void CriaCompra_Purchase_NaoCriaCompraSemIdProduto()
        {
            var ex = Assert.Throws<DomainValidationException>(() => new Purchase(0, 2));

            Assert.Equal("Id produto deve ser informado!", ex.Message);
        }

        [Fact(DisplayName = "Não deve criar compra sem o id da pessoa")]
        public void CriaCompra_Purchase_NaoCriaCompraSemIdPessoa()
        {
            var ex = Assert.Throws<DomainValidationException>(() => new Purchase(1, 0));

            Assert.Equal("Id pessoa deve ser informado!", ex.Message);
        }

        [Fact(DisplayName = "Deve criar compra sem o id")]
        public void CriaCompra_Purchase_DeveCriarCompraSemId()
        {
            var purchase = new Purchase(1, 2);
            Assert.NotNull(purchase);
        }

        [Fact(DisplayName = "Deve criar compra com o id")]
        public void CriaCompra_Purchase_DeveCriarCompraComId()
        {
            var purchase = new Purchase(1, 1, 2);
            Assert.NotNull(purchase);
        }

        [Fact(DisplayName = "Deve editar compra")]
        public void EditaCompra_Edit_DeveEditarCompra()
        {
            var purchase = new Purchase(1, 2, 1);
            purchase.Edit(1, 3, 4);
            Assert.Equal(3, purchase.ProductId);
            Assert.Equal(4, purchase.PersonId);
        }
    }
}
