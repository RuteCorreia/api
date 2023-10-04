using Entities.Entidades.Cadastros.Pilotos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Validators
{
    public class PilotoValidator : AbstractValidator<Piloto>
    {
        public PilotoValidator()
        {
            RuleFor(c => c.NomePiloto)
                .NotEmpty().WithMessage("Insira o nome do piloto")
                .NotNull().WithMessage("Insira o nome do piloto");
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Insira um email")
                .NotNull().WithMessage("Insira um email");
            RuleFor(c => c.Senha)
                .NotEmpty().WithMessage("Insira uma senha")
                .NotNull().WithMessage("Insira uma senha");
            RuleFor(c => c.CDAC)
                .NotEmpty().WithMessage("Insira um CDAC")
                .NotNull().WithMessage("Insira um CDAC");
            RuleFor(c => c.Assinatura)
                .NotEmpty().WithMessage("Insira uma assinatura")
                .NotNull().WithMessage("Insira uma assinatura");
            RuleFor(c => c.PorcentagemComissao)
                .NotEmpty().WithMessage("Insira uma porcentagem de comissão")
                .NotNull().WithMessage("Insira uma porcentagem de comissão");
        }
    }
}
