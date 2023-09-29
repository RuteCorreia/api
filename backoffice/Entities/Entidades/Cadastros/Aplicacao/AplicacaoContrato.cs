using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entidades.Cadastros.Aplicacao
{
    public class AplicacaoContrato
    {
        public int Id { get; set; }

        [ForeignKey("Aplicacao")]
        public int? IdAplicacao { get; set; }
        public string? UF { get; set; }
        public string? Cidade { get; set; }
        public string? NomeCliente { get; set; }
        public string? CPFCliente { get; set; }
        public string? Assinatura { get; set; }
        public virtual Aplicacao Aplicacao { get; set; }
    }
}
