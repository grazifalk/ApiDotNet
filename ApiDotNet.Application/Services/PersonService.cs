using ApiDotNet.Application.DTOs;
using ApiDotNet.Application.DTOs.Validations;
using ApiDotNet.Application.Services.Interfaces;
using ApiDotNet.Domain.Entities;
using ApiDotNet.Domain.FiltersDb;
using ApiDotNet.Domain.Repositories;
using AutoMapper;

namespace ApiDotNet.Application.Services
{
    public class PersonService : IPersonService
    {
        //Colocar quais interfaces vamos utilizar: PersonRepository e Map
        //Vamos mapear a classe que tá vindo (DTO), transformar na entidade e devolver DTO
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public PersonService(IPersonRepository personRepository, IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public async Task<ResultService<PersonDTO>> CreateAsync(PersonDTO personDTO)
        {
            if (personDTO == null)
                return ResultService.Fail<PersonDTO>("Objeto deve ser informado!");

            //validar os campos do objeto para ver se todos os dados obrigatórios estão chegando
            var result = new PersonDTOValidator().Validate(personDTO);
            if (!result.IsValid)
                return ResultService.RequestError<PersonDTO>("Problemas de validade!", result);

            //fazer o mapping para inserir a pessoa, na hora de inserir no banco tem que ser a entidade e não o DTO
            var person = _mapper.Map<Person>(personDTO); //transforma DTO em entidade
            var data = await _personRepository.CreateAsync(person);//insere a entidade e pega o retorno da entidade com id gerado
            return ResultService.Ok<PersonDTO>(_mapper.Map<PersonDTO>(data));//devolvi a entidade e retorno o DTO pro controller mostrando na tela em forma de DTO
        }

        public async Task<ResultService> DeleteAsync(int id)
        {
            //checar se o id existe
            var person = await _personRepository.GetByIdAsync(id);
            if (person == null)
                return ResultService.Fail("Pessoa não encontrada!");
            await _personRepository.DeleteAsync(person);
            return ResultService.Ok($"Pessoa do id {id} foi deletada!");
        }

        public async Task<ResultService<ICollection<PersonDTO>>> GetAsync()
        {
            //primeiro buscar pessoas dos repositórios e depois retornar em DTO
            var people = await _personRepository.GetPeopleAsync(); //busca entidade
            return ResultService.Ok<ICollection<PersonDTO>>(_mapper.Map<ICollection<PersonDTO>>(people)); //devolve DTO
        }

        public async Task<ResultService<PersonDTO>> GetByIdAsync(int id)
        {
            var person = await _personRepository.GetByIdAsync(id);
            if (person == null)
                return ResultService.Fail<PersonDTO>("Pessoa não encontrada!");
            return ResultService.Ok(_mapper.Map<PersonDTO>(person));
        }

        public async Task<ResultService<PagedBaseResponseDTO<PersonDTO>>> GetPagedAsync(PersonFilterDb personFilterDb)
        {
            var peoplePaged = await _personRepository.GetPagedAsync(personFilterDb); //chamamos o método que vai retornar nossos dados paginados 
            var result = new PagedBaseResponseDTO<PersonDTO>(peoplePaged.TotalRegisters,
                                                  _mapper.Map<List<PersonDTO>>(peoplePaged.Data)); //pegamos os dados e transformamos a entidade em DTO já criando a classe result
            return ResultService.Ok(result);
        }

        public async Task<ResultService> UpdateAsync(PersonDTO personDTO)
        {
            if (personDTO == null)
                return ResultService.Fail("Objeto deve ser informado!");

            //fazer validações dos atributos obrigatórios
            var validation = new PersonDTOValidator().Validate(personDTO);
            if (!validation.IsValid)
                return ResultService.RequestError("Problema com a validação dos campos.", validation);

            //buscar pessoa
            var person = await _personRepository.GetByIdAsync(personDTO.Id);
            if (person == null)
                return ResultService.Fail("Pessoa não encontrada!");

            //caso pessoa seja encontrada, vamos usar o map para pegar as pessoas do dto e jogar pra dentro da nossa pessoa
            //na edição precisamos ter o método mapeado pelo entity framework e assim conseguimos editar o objeto (vamos manter o objeto mapeado e atualizar, sem criar um novo)
            //vamos fazer com que os dados da DTO vá pra pessoa sem criar uma nova e sim na que já está mapeada
            // na criação era: var person = _mapper.Map<Person>(personDTO);
            person = _mapper.Map<PersonDTO, Person>(personDTO, person); //edição, mantendo estado da person, joga os dados da dto na entidade
            await _personRepository.UpdateAsync(person);
            return ResultService.Ok("Pessoa editada");
        }
    }
}
