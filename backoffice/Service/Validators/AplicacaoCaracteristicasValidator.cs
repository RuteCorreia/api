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
    public class AplicacaoCaracteristicasValidator : AbstractValidator<AplicacaoCaracteristicas>
    {
        public AplicacaoCaracteristicasValidator()
        {
            RuleFor(c => c.TipoDeServico)
                .NotEmpty().WithMessage("Insira um tipo de serviço")
                .NotNull().WithMessage("Insira um tipo de serviço");
        }
    }
}
