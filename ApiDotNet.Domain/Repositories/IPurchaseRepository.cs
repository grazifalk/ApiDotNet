using ApiDotNet.Domain.Entities;

namespace ApiDotNet.Domain.Repositories
{
    public interface IPurchaseRepository
    {
        Task<Purchase> CreateAsync(Purchase purchase);
        Task DeleteAsync(Purchase purchase);
        Task EditAsync(Purchase purchase);
        Task<Purchase> GetByIdAsync(int id);
        Task<ICollection<Purchase>> GetByPersonIdAsync(int personId);
        Task<ICollection<Purchase>> GetByProductIdAsync(int productId);
        Task<ICollection<Purchase>> GetAllAsync();
    }
}
