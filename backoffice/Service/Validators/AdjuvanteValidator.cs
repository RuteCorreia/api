using Entities.Entidades.Cadastros.Adjuvante;
using Entities.Entidades.Cadastros.Empresa;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Validators
{
    public class AdjuvanteValidator : AbstractValidator<Adjuvante>
    {
        public AdjuvanteValidator()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("Insira um Nome")
                .NotNull().WithMessage("Insira um Nome");
        }
    }
}
