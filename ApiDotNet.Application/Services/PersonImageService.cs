using ApiDotNet.Application.DTOs;
using ApiDotNet.Application.DTOs.Validations;
using ApiDotNet.Application.Services.Interfaces;
using ApiDotNet.Domain.Entities;
using ApiDotNet.Domain.Integrations;
using ApiDotNet.Domain.Repositories;

namespace ApiDotNet.Application.Services
{
    public class PersonImageService : IPersonImageService
    {
        private readonly IPersonImageRepository _personImageRepository;
        private readonly IPersonRepository _personRepository;
        //chamar serviço de integração
        private readonly ISavePersonImage _savePersonImage;

        public PersonImageService(IPersonImageRepository personImageRepository, IPersonRepository personRepository, ISavePersonImage savePersonImage)
        {
            _personImageRepository = personImageRepository;
            _personRepository = personRepository;
            _savePersonImage = savePersonImage;
        }

        public async Task<ResultService> CreateImageAsync(PersonImageDTO personImageDTO)
        {
            //validar se o objeto é nulo
            if (personImageDTO == null)
                return ResultService.Fail("Objeto deve ser informado!");

            //validar DTO
            var validation = new PersonImageDTOValidator().Validate(personImageDTO);

            //verificar se não for válida
            if (!validation.IsValid)
                return ResultService.RequestError("Problemas com a validação", validation);

            //validar pessoa
            var person = await _personRepository.GetByIdAsync(personImageDTO.PersonId);
            if (person == null)
                return ResultService.Fail("Pessoa não encontrada!");

            //inserir no serviço
            var pathImage = _savePersonImage.Save(personImageDTO.Image); //chama o método que salva a imagem e retorna caminho da imagem passando a imagem que é o base64 / URI
            var personImage = new PersonImage(person.Id, pathImage, null); //adicionar
            await _personImageRepository.CreateAsync(personImage); //salvar
            return ResultService.Ok("Imagem salva!");
        }

        public async Task<ResultService> CreateImageBase64Async(PersonImageDTO personImageDTO)
        {
            if (personImageDTO == null)
                return ResultService.Fail("Objeto deve ser informado!");

            var validations = new PersonImageDTOValidator().Validate(personImageDTO);
            if (!validations.IsValid)
                return ResultService.RequestError("Problemas de validação", validations);

            //achar pessoa e verificar se existe
            var person = await _personRepository.GetByIdAsync(personImageDTO.PersonId);
            if (person == null)
                return ResultService.Fail("Id pessoa não encontrado.");

            //se existir vamos criar o objeto dela e vamos passar nossos campos (idpessoa e imagem base 64)
            var personImage = new PersonImage(person.Id, null, personImageDTO.Image);
            //vamos salvar o objeto no banco
            await _personImageRepository.CreateAsync(personImage);
            return ResultService.Ok("Imagem em base64 salva!");
        }
    }
}
