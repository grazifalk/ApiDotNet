using ApiDotNet.Domain.Entities;
using ApiDotNet.Infra.Data.Context;
using ApiDotNet.Infra.Data.Repositories;
using ApiDotNet.UnitTest.Infra.Data.Context;

namespace ApiDotNet.UnitTest.Infra.Data.Repositories
{
    public class ProductRepositoryTest
    {
        private ApplicationDbContext _context;

        [Fact(DisplayName ="Deve criar um produto")]
        public async Task SalvaProduto_ProductRepository_DeveSalvarUmProdutoNoBancoDeDados()
        {
            //criar objeto de produto
            //colocar "m" porque valor é decimal e estamos passando número inteiro
            var product = new Product("Teste produto", "45454", 20m);
            //criar banco em memória
            _context = TesteDatabaseInMemory.GetDatabase();

            //usar método create do repositório de produto
            var productRepository = new ProductRepository(_context);
            //chamar o método
            await productRepository.CreateAsync(product);

            //checar se criou ou não
            var productResult = _context.Products.FirstOrDefault();
            //comparar a expectativa e o atual
            Assert.Equal(product.Name, productResult.Name);
        }

        [Fact(DisplayName = "Deve listar um produto pelo seu id")]
        public async Task ListaProduto_ProductRepository_DeveBuscarUmProdutoPeloSeuID()
        {
            //criar objeto de produto
            var product = new Product("Teste produto", "45454", 20m);
            //criar banco em memória
            _context = TesteDatabaseInMemory.GetDatabase();
            _context.Products.Add(product);
            _context.SaveChanges();

            var productRepository = new ProductRepository(_context);
            //chamar o método de busca
            var productResult = await productRepository.GetByIdAsync(product.Id);

            //comparar a expectativa e o atual
            Assert.Equal(product.Name, productResult.Name);
        }

        [Fact(DisplayName = "Deve editar um produto")]
        public async Task EditaProduto_ProductRepository_DeveEditarUmProduto()
        {
            //criar objeto de produto
            var product = new Product("Teste produto", "45454", 20m);
            //criar banco em memória
            _context = TesteDatabaseInMemory.GetDatabase();
            _context.Products.Add(product);
            _context.SaveChanges();

            product.Edit("Teste 2 produto", "45454", 20m);

            var productRepository = new ProductRepository(_context);
            //chamar o método de edit
            await productRepository.UpdateAsync(product);

            var productResult = _context.Products.FirstOrDefault();

            //comparar a expectativa e o atual
            Assert.Equal("Teste 2 produto", productResult.Name);
        }

        [Fact(DisplayName = "Deve remover um produto")]
        public async Task DeleteProduto_ProductRepository_DeveDeletarUmProduto()
        {
            //criar objeto de produto
            var product = new Product("Teste produto", "45454", 20m);
            //criar banco em memória
            _context = TesteDatabaseInMemory.GetDatabase();
            _context.Products.Add(product);
            _context.SaveChanges();

            var productRepository = new ProductRepository(_context);
            //chamar o método de busca
            await productRepository.DeleteAsync(product);

            //comparar a expectativa e o atual
            Assert.Equal(0, _context.Products.Count());
        }
    }
}
