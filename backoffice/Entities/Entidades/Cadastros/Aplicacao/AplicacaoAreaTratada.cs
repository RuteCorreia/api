using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entidades.Cadastros.Aplicacao
{
    public class AplicacaoAreaTratada
    {
        public int Id { get; set; }

        [ForeignKey("Aplicacao")]
        public int? IdAplicacao { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Localizacao { get; set; }
        public decimal? Extensao { get; set; }
        public virtual Aplicacao Aplicacao { get; set; }

    }
}
