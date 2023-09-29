using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entidades.Cadastros.Aplicacao
{
    public class AplicacaoCroqui
    {
        public int Id { get; set; }

        [ForeignKey("Aplicacao")]
        public int? IdAplicacao { get; set; }
        public string Desenho { get; set; }
        public int? IdMapa { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public virtual Aplicacao Aplicacao { get; set; }
    }
}
