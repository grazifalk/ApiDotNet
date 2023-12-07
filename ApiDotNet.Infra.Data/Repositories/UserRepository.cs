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
        public async Task<User?> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            return await _db.Users
                .Include(x => x.UserPermissions).ThenInclude(x => x.Permission)
                .FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
        }

        //vamos fazer um join, para através do usuário acessar as permissões
        //além de incluir a lista, vamos fazer um join que não será com usuário e sim com usuáriopermissão
        //quando adicionamos o Include, o relacionamento é com o usuário
        //quando usamos o ThenInclude, o relacionamento é com o último Include que colocamos
        //fazendo isso garantimos que quando estivermos dentro da lista de usuários a gente consiga acessar as informações de permissões, ou seja, o nome visual o id etc
    }
}
