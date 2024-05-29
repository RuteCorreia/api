using Entities.Entidades.Cadastros.Aeronaves;
using Entities.Entidades.Cadastros.Empresa;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Validators
{
    public class AeronaveValidator : AbstractValidator<Aeronave>
    {
        public AeronaveValidator()
        {
            RuleFor(c => c.Horimetro)
                .NotEmpty().WithMessage("Insira um Horimetro")
                .NotNull().WithMessage("Insira um Horimetro");
        }
    }
}
