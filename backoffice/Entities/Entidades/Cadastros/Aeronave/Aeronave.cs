using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entidades.Cadastros.Aeronaves
{
    public class Aeronave
    {
        public int Id { get; set; }

        [ForeignKey("Empresa")]
        public int? IdEmpresa { get; set; }
        public string Prefixo { get; set; }
        public string Combustivel { get; set; }
        public int? CapacidadeDeCarga { get; set; }
        public string Horimetro { get; set; }

        public virtual Empresa.Empresa Empresa { get; set; }
    }
}
