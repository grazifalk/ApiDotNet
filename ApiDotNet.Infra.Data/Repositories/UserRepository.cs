using ApiDotNet.Domain.Entities;
using ApiDotNet.Domain.Repositories;
using ApiDotNet.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ApiDotNet.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        //sobrecarga do ApplicationDbContext
        private readonly ApplicationDbContext _db;
        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        //vai buscar usuário no banco e vai validar se o email e senha tem no banco
        public async Task<User> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            return await _db.Users.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
        }
    }
}
