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
    public class AplicacaoAreaTratadaValidator : AbstractValidator<AplicacaoAreaTratada>
    {
        public AplicacaoAreaTratadaValidator()
        {
            RuleFor(c => c.Localizacao)
                .NotEmpty().WithMessage("Insira uma Localização")
                .NotNull().WithMessage("Insira uma Localização");
        }
    {
    }
}
