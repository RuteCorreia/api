using Entities.Entidades.Cadastros.Aplicacao;
using Entities.Entidades.Cadastros.Empresa;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Validators
{
    public class AplicacaoCroquiValidator : AbstractValidator<AplicacaoCroqui>
    {
        public AplicacaoCroquiValidator()
        {
            RuleFor(c => c.Latitude)
                .NotEmpty().WithMessage("Insira uma Latitude")
                .NotNull().WithMessage("Insira uma Latitude");
        }
    }
}
