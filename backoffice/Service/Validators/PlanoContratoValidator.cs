using Entities.Entidades.Cadastros.Empresa;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Validators
{
    public class PlanoContratoValidator : AbstractValidator<PlanoDeContrato>
    {
        public PlanoContratoValidator()
        {
            RuleFor(c => c.NomeDoPlano)
                .NotEmpty().WithMessage("Insira um Nome")
                .NotNull().WithMessage("Insira um Nome");
        }
    }
}
