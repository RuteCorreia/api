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
            RuleFor(c => c.Username)
                .NotEmpty().WithMessage("Insira um Nome")
                .NotNull().WithMessage("Insira um Nome");
        }
    }
}
