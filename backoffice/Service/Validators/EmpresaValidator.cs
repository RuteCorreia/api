using Entities.Entidades.Cadastros.Empresa;
using Entities.Entidades.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Validators
{
    public class EmpresaValidator : AbstractValidator<Empresa>
    {
        public EmpresaValidator()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("Insira um Nome")
                .NotNull().WithMessage("Insira um Nome");
            RuleFor(c => c.Imagem)
                .NotEmpty().WithMessage("Insira uma Imagem")
                .NotNull().WithMessage("Insira uma Imagem");
            RuleFor(c => c.Imagem)
                .NotEmpty().WithMessage("Informe o plano contratado")
                .NotNull().WithMessage("Informe o plano contratado");
        }
    }
}
