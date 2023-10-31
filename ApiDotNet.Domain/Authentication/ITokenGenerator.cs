using ApiDotNet.Domain.Entities;

namespace ApiDotNet.Domain.Authentication
{
    public interface ITokenGenerator
    {
        dynamic Generator(User user);
    }
}
