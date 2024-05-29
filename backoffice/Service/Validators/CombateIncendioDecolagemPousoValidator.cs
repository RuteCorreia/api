using Entities.Entidades.Cadastros.CombateIncendio;
using Entities.Entidades.Cadastros.Empresa;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Validators
{
    public class CombateIncendioDecolagemPousoValidator : AbstractValidator<CombateIncendioDecolagemPouso>
    {
        public CombateIncendioDecolagemPousoValidator()
        {
            RuleFor(c => c.DecolagemHorario)
                .NotEmpty().WithMessage("Insira um horario de decolagem")
                .NotNull().WithMessage("Insira um horario de decolagem");
        }
    }
}
