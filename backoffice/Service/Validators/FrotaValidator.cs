using Entities.Entidades.Cadastros.Empresa;
using Entities.Entidades.Cadastros.Frota;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Validators
{
    public class FrotaValidator : AbstractValidator<Frota>
    {
        public FrotaValidator()
        {
            RuleFor(c => c.NomeVeiculo)
                .NotEmpty().WithMessage("Insira um Nome")
                .NotNull().WithMessage("Insira um Nome");
        }
    {
    }
}
