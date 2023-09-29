using Entities.Entidades.Cadastros.Pistas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entidades.Cadastros.Aplicacao
{
    public class AplicacaoRelatorio
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Aplicacao")]
        public int? IdAplicacao { get; set; }

        [ForeignKey("Pista")]
        public int? IdPista { get; set; }
        public decimal? Dosagem { get; set; }
        public string KG_LT { get; set; }
        public int? VolumeAplicacao { get; set; }
        public decimal? TotalAreaAplicada { get; set; }
        public string Alteracoes_Observacoes { get; set; }
        public virtual Aplicacao Aplicacao { get; set; }
        public virtual Pista Pista { get; set; }
    }
}
