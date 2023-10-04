using Entities.Entidades.Cadastros.Executores;
using Entities.Entidades.Cadastros.Pilotos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Validators
{
    public class ExecutorValidator : AbstractValidator<Executor>
    {
        public ExecutorValidator()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("Insira um Nome")
                .NotNull().WithMessage("Insira um Nome");
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Insira um email")
                .NotNull().WithMessage("Insira um email");
            RuleFor(c => c.Senha)
                .NotEmpty().WithMessage("Insira uma senha")
                .NotNull().WithMessage("Insira uma senha");
            RuleFor(c => c.CFTA)
                .NotEmpty().WithMessage("Insira um CFTA")
                .NotNull().WithMessage("Insira um CFTA");
            RuleFor(c => c.Assinatura)
                .NotEmpty().WithMessage("Insira uma assinatura")
                .NotNull().WithMessage("Insira uma assinatura");
        }
    }
}
