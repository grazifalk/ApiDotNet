using ApiDotNet.Application.DTOs;

namespace ApiDotNet.Application.Services.Interfaces
{
    public interface IPersonImageService
    {
        Task<ResultService> CreateImageBase64Async(PersonImageDTO personImageDTO);
        Task<ResultService> CreateImageAsync(PersonImageDTO personImageDTO);
    }
}
