using Entities.Entidades.Cadastros.Aeronaves;
using Entities.Entidades.Cadastros.Pilotos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entidades.Cadastros.Controle_De_Frota
{
    public class ControleDeFrota
    {
        public int Id { get; set; }
        public string? Observacao { get; set; }
        public DateTime? Data { get; set; }

        [ForeignKey("Frota")]
        public int? IdFrota { get; set; }

        [ForeignKey("Aeronave")]
        public int? IdAeronave { get; set; }
        public int? KmInicial { get; set; }
        public int? LocalInicial { get; set; }
        public string? LocalizacaoPistaLat { get; set; }
        public string? LocalizacaoPistaLon { get; set; }
        public int? KmFinal { get; set; }
        public int? HorimetroInicial { get; set; }
        public int? HorimetroFinal { get; set; }
        public string? Combustivel { get; set; }
        public int? QtdeCombustivel { get; set; }
        public int? QtdeHectare { get; set; }

        [ForeignKey("Piloto")]
        public int? IdPiloto { get; set; }
        public virtual Frota.Frota Frota { get; set; }
        public virtual Aeronave Aeronave { get; set; }
        public virtual Piloto Piloto { get; set; }
    }
}
