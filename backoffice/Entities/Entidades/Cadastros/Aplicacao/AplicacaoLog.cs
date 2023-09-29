using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entidades.Cadastros.Aplicacao
{
    public class AplicacaoLog
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Aplicacao")]
        public int? IdAplicacao { get; set; }
        public DateTime? Data { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public virtual Aplicacao Aplicacao { get; set; }
    }
}
