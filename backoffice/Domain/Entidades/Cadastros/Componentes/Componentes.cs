using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Domain.Entidades.Cadastros.Aeronave;

namespace Domain.Entidades.Cadastros.Componentes
{
    public class Componentes
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Aeronave")]
        public int? IdAeronave { get; set; }
        public string? NomeComponente { get; set; }
        public string? Grupo { get; set; }
        public string? PartNumber { get; set; }
        public string? SerialNumber { get; set; }
        public string? TLV { get; set; }
        public string? TBO { get; set; }
        public string? EnumTLV { get; set; }
        public string? EnumTBO { get; set; }
        public string? UltimaInspecao { get; set; }
        public string? PrazoParaInspecao { get; set; }
        public string? TSN { get; set; }
        public string? TSO { get; set; }

        [ForeignKey("Empresa")]
        public int? IdEmpresa { get; set; }

        [JsonIgnore]
        public virtual Empresa.Empresa? Empresa { get; set; }

        [JsonIgnore]
        public virtual Aeronave.Aeronave? Aeronave { get; set; }
    }
}
