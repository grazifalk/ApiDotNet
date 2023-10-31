﻿using FluentValidation;

namespace ApiDotNet.Application.DTOs.Validations
{
    public class UserDTOValidator : AbstractValidator<UserDTO>
    {
        public UserDTOValidator()
        {
            //implementar regras
            RuleFor(x => x.Email).NotEmpty().NotNull().WithMessage("Email deve ser informado!");
            RuleFor(x => x.Password).NotEmpty().NotNull().WithMessage("Password deve ser informado!");
        }
    }
}
