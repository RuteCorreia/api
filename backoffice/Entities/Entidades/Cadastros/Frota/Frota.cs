using Entities.Entidades.Cadastros.Empresa;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entidades.Cadastros.Frota
{
    public class Frota
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Empresa")]
        public int? IdEmpresa { get; set; }
        public string NomeVeiculo { get; set; }
        public string Placa { get; set; }
        public string Combustivel { get; set; }
        public string Hodometro { get; set; }
        public virtual Empresa.Empresa Empresa { get; set; }
    }
}
