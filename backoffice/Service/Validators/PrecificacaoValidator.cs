using Entities.Entidades.Cadastros.Empresa;
using Entities.Entidades.Cadastros.Precificacao;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Validators
{
    public class PrecificacaoValidator : AbstractValidator<Precificacao>
    {
        public PrecificacaoValidator()
        {
            RuleFor(c => c.PrecoHA)
                .NotEmpty().WithMessage("Insira um Nome")
                .NotNull().WithMessage("Insira um Nome");
        }
    }
}
