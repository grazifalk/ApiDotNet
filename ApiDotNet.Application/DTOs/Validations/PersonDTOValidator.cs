using FluentValidation;

namespace ApiDotNet.Application.DTOs.Validations
{
    public class PersonDTOValidator : AbstractValidator<PersonDTO>
    {
        public PersonDTOValidator()
        {
            RuleFor(x => x.Document).NotEmpty().NotNull().WithMessage("Documento deve ser informado!");
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Nome deve ser informado!");
            RuleFor(x => x.Phone).NotEmpty().NotNull().WithMessage("Celular deve ser informado!");
        }
    }
}
