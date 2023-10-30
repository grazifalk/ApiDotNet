using ApiDotNet.Domain.Entities;

namespace ApiDotNet.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAndPasswordAsync(string email, string password);
    }
}
