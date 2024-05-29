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
    public class AplicacaoRecomendacoesTecnicasValidator : AbstractValidator<AplicacaoRecomendacoesTecnicas>
    {
        public AplicacaoRecomendacoesTecnicasValidator()
        {
            RuleFor(c => c.QtdeVeiculante)
                .NotEmpty().WithMessage("Insira uma quantidade de veiculantes")
                .NotNull().WithMessage("Insira uma quantidade de veiculantes");
        }
    }
}
