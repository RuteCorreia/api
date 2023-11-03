using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.Entidades.Cadastros.CombateIncendio
{
    public class CombateIncendioDecolagemPouso
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("CombateIncendio")]
        public int? IdCombateIncendio { get; set; }
        public DateTime? DecolagemHorario { get; set; }
        public string? DecolagemHorimetro { get; set; }
        public DateTime? PousoHorario { get; set; }
        public string? PousoHorimetro { get; set; }

        [JsonIgnore]
        public virtual CombateIncendio? CombateIncendio { get; set; }
    }
}
