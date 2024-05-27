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
    public class AplicacaoLogValidator : AbstractValidator<AplicacaoLog>
    {
        public AplicacaoLogValidator()
        {
            RuleFor(c => c.Descricao)
                .NotEmpty().WithMessage("Insira uma Descrição")
                .NotNull().WithMessage("Insira uma Descrição");
        }
    }
}
