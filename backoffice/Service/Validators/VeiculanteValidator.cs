using Entities.Entidades.Cadastros.Empresa;
using Entities.Entidades.Cadastros.Veiculante;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Validators
{
    public class VeiculanteValidator : AbstractValidator<Veiculante>
    {
        public VeiculanteValidator()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("Insira um Nome")
                .NotNull().WithMessage("Insira um Nome");
        }
    }
}
