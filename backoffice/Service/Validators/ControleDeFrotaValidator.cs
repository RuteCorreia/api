using Entities.Entidades.Cadastros.Controle_De_Frota;
using Entities.Entidades.Cadastros.Empresa;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Validators
{
    public class ControleDeFrotaValidator : AbstractValidator<ControleDeFrota>
    {
        public ControleDeFrotaValidator()
        {
            RuleFor(c => c.Combustivel)
                .NotEmpty().WithMessage("Insira um tipo de combustivel")
                .NotNull().WithMessage("Insira um tipo de combustivel");
        }
    }
}
