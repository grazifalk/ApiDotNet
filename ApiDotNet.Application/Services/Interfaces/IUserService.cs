using ApiDotNet.Application.DTOs;

namespace ApiDotNet.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<ResultService<dynamic>> GenerateTokenAsync(UserDTO userDTO);
    }
}
