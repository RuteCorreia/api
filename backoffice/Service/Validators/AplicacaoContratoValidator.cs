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
    public class AplicacaoContratoValidator : AbstractValidator<AplicacaoContrato>
    {
        public AplicacaoContratoValidator()
        {
            RuleFor(c => c.NomeCliente)
                .NotEmpty().WithMessage("Insira um Nome de cliente")
                .NotNull().WithMessage("Insira um Nome de cliente");
        }
    }
}
