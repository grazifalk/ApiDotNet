using ApiDotNet.Application.DTOs;
using ApiDotNet.Application.DTOs.Validations;
using ApiDotNet.Application.Services.Interfaces;
using ApiDotNet.Domain.Authentication;
using ApiDotNet.Domain.Repositories;

namespace ApiDotNet.Application.Services
{
    public class UserService : IUserService
    {
        //repositório de usuário, para validar se o email e senha tem no banco e do token para podermos gerar
        private readonly IUserRepository _userRepository;
        private readonly ITokenGenerator _tokenGenerator;

        public UserService(IUserRepository userRepository, ITokenGenerator tokenGenerator)
        {
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<ResultService<dynamic>> GenerateTokenAsync(UserDTO userDTO)
        {
            //primeiro validar se DTO tem informação
            if (userDTO == null)
                return ResultService.Fail<dynamic>("Objeto deve ser informado!");

            //validar dados que estão vindo
            var validator = new UserDTOValidator().Validate(userDTO);
            if (!validator.IsValid)
                return ResultService.RequestError<dynamic>("Problemas com a validação!", validator);

            //checar se usuário e senha tem na nossa vase
            var user = await _userRepository.GetUserByEmailAndPasswordAsync(userDTO.Email, userDTO.Password);
            if (user == null)
                return ResultService.Fail<dynamic>("Usuário ou senha não encontrado!");

            //se encontrarmos o usuário, vamos gerar o token
            return ResultService.Ok(_tokenGenerator.Generator(user));
            
        }
    }
}
