using Entities.Entidades.Cadastros.Pilotos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Validators
{
    public class PilotoValidator : AbstractValidator<Piloto>
    {
        public PilotoValidator()
        {
            RuleFor(c => c.NomePiloto)
                .NotEmpty().WithMessage("Insira um Nome")
                .NotNull().WithMessage("Insira um Nome");
        }
    }
}
