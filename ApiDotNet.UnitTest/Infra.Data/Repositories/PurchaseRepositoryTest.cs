using ApiDotNet.Domain.Entities;
using ApiDotNet.Infra.Data.Context;
using ApiDotNet.Infra.Data.Repositories;
using ApiDotNet.UnitTest.Infra.Data.Context;

namespace ApiDotNet.UnitTest.Infra.Data.Repositories
{
    public class PurchaseRepositoryTest
    {
        private ApplicationDbContext _context;

        [Fact(DisplayName ="Deve criar uma compra")]
        public async Task SalvarCompraDb_CreateAsync_DeveSalvarUmaCompraNoBancoDeDados()
        {
            var purchase = new Purchase(1, 1);
            _context = TesteDatabaseInMemory.GetDatabase();

            var purchaseRepository = new PurchaseRepository(_context);
            await purchaseRepository.CreateAsync(purchase);

            var purchaseResult = _context.Purchases.FirstOrDefault();
            Assert.Equal(1, purchaseResult.PersonId);
        }

        [Fact(DisplayName ="Deve editar compra")]
        public async Task EditaCompra_EditAsync_DeveEditarUmaCompraNoBancoDeDados()
        {
            var purchase = new Purchase(1, 1);
            _context = TesteDatabaseInMemory.GetDatabase();
            _context.Add(purchase);
            _context.SaveChanges();

            purchase.Edit(1, 2, 3);

            var purchaseRepository = new PurchaseRepository(_context);
            await purchaseRepository.EditAsync(purchase);

            var purchaseResult = _context.Purchases.FirstOrDefault();
            Assert.Equal(3, purchaseResult.PersonId);
        }

        [Fact(DisplayName ="Deve retornar uma compra pelo seu id")]
        public async Task RetornaCompra_GetByIdAsync_DeveRetornarUmaCompra()
        {
            var product = new Product("Teste Produto", "45545", 20m);
            var person = new Person("12345678989", "Teste Pessoa", "999999999");
            _context = TesteDatabaseInMemory.GetDatabase();
            _context.Add(product);
            _context.Add(person);
            _context.SaveChanges();

            var purchase = new Purchase(1, 1);
            _context.Add(purchase);
            _context.SaveChanges();

            var purchaseRepository = new PurchaseRepository(_context);
            var purchaseResult = await purchaseRepository.GetByIdAsync(purchase.Id);

            Assert.Equal(1, purchaseResult.PersonId);
        }
    }
}
