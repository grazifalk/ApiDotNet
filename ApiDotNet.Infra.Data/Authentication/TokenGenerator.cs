using ApiDotNet.Domain.Authentication;
using ApiDotNet.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiDotNet.Infra.Data.Authentication
{
    public class TokenGenerator : ITokenGenerator
    {
        //implementar informações que vamos colocar dentro do token
        public dynamic Generator(User user)
        {
            var permission = string.Join(",", user.UserPermissions.Select(x => x.Permission?.PermissionName));
            var claims = new List<Claim>
            {
                new Claim("Email", user.Email),
                new Claim("Id", user.Id.ToString()),
                new Claim("Permissoes", permission)
            };

            //criar expiração
            var expires = DateTime.Now.AddDays(1);

            //gerar a chave do nosso Token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("projetoDotNetCoreprojetoDotNetCoreprojetoDotNetCore"));

            var tokenData = new JwtSecurityToken(
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
                expires: expires,
                claims: claims
                );

            //gerar token
            var token = new JwtSecurityTokenHandler().WriteToken(tokenData);

            //retornar nosso dynamic
            return new
            {
                access_token = token,
                expirations = expires
            };
        }
    }
}
