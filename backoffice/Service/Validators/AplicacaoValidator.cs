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
    public class AplicacaoValidator : AbstractValidator<Aplicacao>
    {
        public AplicacaoValidator()
        {
            RuleFor(c => c.IdCliente)
                .NotEmpty().WithMessage("Insira um Id de Cliente")
                .NotNull().WithMessage("Insira um Id de Cliente");
        }
    }
}
