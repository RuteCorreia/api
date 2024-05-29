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
    public class AplicacaoRelatorioItemValidator : AbstractValidator<AplicacaoRelatorioItem>
    {
        public AplicacaoRelatorioItemValidator()
        {
            RuleFor(c => c.HorimetroInicial)
                .NotEmpty().WithMessage("Insira o Horimetro Inicial")
                .NotNull().WithMessage("Insira o Horimetro Inicial");
        }
    }
}
