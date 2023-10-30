using ApiDotNet.Domain.Entities;
using ApiDotNet.Domain.Repositories;
using ApiDotNet.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ApiDotNet.Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            _db.Add(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task DeleteAsync(Product product)
        {
            _db.Remove(product);
            await _db.SaveChangesAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _db.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> GetIdByCodErpAsync(string codErp)
        {
            //buscamos o codErp, se tiver informação retorna o Id e se for nulo retorna zero
            return (await _db.Products.FirstOrDefaultAsync(x => x.CodErp == codErp))?.Id ?? 0;
        }

        public async Task<ICollection<Product>> GetProductAsync()
        {
            return await _db.Products.ToListAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _db.Update(product);
            await _db.SaveChangesAsync();
        }
    }
}
