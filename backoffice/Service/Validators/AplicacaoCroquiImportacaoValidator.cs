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
    public class AplicacaoCroquiImportacaoValidator : AbstractValidator<AplicacaoCroquiImportacao>
    {
        public AplicacaoCroquiImportacaoValidator()
        {
            RuleFor(c => c.Arquivo)
                .NotEmpty().WithMessage("Insira um Arquivo")
                .NotNull().WithMessage("Insira um Arquivo");
        }
    }
}
