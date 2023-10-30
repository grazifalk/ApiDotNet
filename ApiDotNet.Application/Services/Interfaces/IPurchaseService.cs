using ApiDotNet.Application.DTOs;

namespace ApiDotNet.Application.Services.Interfaces
{
    public interface IPurchaseService
    {
        Task<ResultService<PurchaseDTO>> CreateAsync(PurchaseDTO purchaseDTO);
        Task<ResultService<PurchaseDetailsDTO>> GetByIdAsync(int id);
        Task<ResultService<ICollection<PurchaseDetailsDTO>>> GetAsync();
        Task<ResultService<PurchaseDTO>> UpdateAsync(PurchaseDTO purchaseDTO);
        Task<ResultService> DeleteAsync(int id);

    }
}
