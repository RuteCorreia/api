using Entities.Entidades.Cadastros.Empresa;
using Entities.Entidades.Cadastros.Engenheiros;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Validators
{
    public class EngenheiroValidator : AbstractValidator<Engenheiro>
    {
        public EngenheiroValidator()
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
            RuleFor(c => c.CREA)
                .NotEmpty().WithMessage("Insira um CREA")
                .NotNull().WithMessage("Insira um CREA");
            RuleFor(c => c.Assinatura)
                .NotEmpty().WithMessage("Insira uma assinatura")
                .NotNull().WithMessage("Insira uma assinatura");
        }
    }
}
