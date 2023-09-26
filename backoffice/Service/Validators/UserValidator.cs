using Entities.Entidades.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("Insira um Nome")
                .NotNull().WithMessage("Insira um Nome");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Insira um email.")
                .NotNull().WithMessage("Insira um email.");

            RuleFor(c => c.Senha)
                .NotEmpty().WithMessage("Insira uma senha valida.")
                .NotNull().WithMessage("Insira uma senha valida.");
        }
    }
}
