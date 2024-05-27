using Entities.Entidades.Cadastros.Pistas;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Validators
{
    public class PistaValidator : AbstractValidator<Pista>
    {
        public PistaValidator()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("Insira um Nome")
                .NotNull().WithMessage("Insira um Nome");
        }
    }
}
