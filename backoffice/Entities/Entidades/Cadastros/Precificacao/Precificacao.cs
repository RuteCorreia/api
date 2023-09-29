using Entities.Entidades.Cadastros.Empresa;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entidades.Cadastros.Precificacao
{
    public class Precificacao
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Empresa")]
        public int? IdEmpresa { get; set; }
        public string DistanciaPista { get; set; }
        public decimal? PrecoHA { get; set; }
        public virtual Empresa.Empresa Empresa { get; set; }

    }
}
