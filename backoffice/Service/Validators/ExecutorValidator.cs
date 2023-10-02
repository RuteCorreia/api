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
        }
    }
}
