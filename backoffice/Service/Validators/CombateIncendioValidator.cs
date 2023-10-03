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
    public class CombateIncendioValidator : AbstractValidator<CombateIncendio>
    {
        public CombateIncendioValidator()
        {
            RuleFor(c => c.Aviso)
                .NotEmpty().WithMessage("Insira um Aviso")
                .NotNull().WithMessage("Insira um Aviso");
        }
    }
}
