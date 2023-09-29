using Entities.Entidades.Cadastros.Cidades;
using Entities.Entidades.Cadastros.Estados;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entidades.Cadastros.Aplicacao
{
    public class AplicacaoContrato
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Aplicacao")]
        public int? IdAplicacao { get; set; }

        [ForeignKey("Estado")]
        public int? IdUF { get; set; }

        [ForeignKey("Cidade")]
        public int? IdCidade { get; set; }
        public string? NomeCliente { get; set; }
        public string? CPFCliente { get; set; }
        public string? Assinatura { get; set; }
        public virtual Aplicacao Aplicacao { get; set; }
        public virtual Estados.Estados Estado { get; set; }
        public virtual Cidades.Cidades Cidade { get; set; }
    }
}
