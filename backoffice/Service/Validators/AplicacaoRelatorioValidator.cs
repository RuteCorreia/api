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
    public class AplicacaoRelatorioValidator : AbstractValidator<AplicacaoRelatorio>
    {
        public AplicacaoRelatorioValidator()
        {
            RuleFor(c => c.Alteracoes_Observacoes)
                .NotEmpty().WithMessage("Insira uma Observação")
                .NotNull().WithMessage("Insira uma Observação");
        }
    }
}
