using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.CombateIncendio
{
    public class CombateIncendioPista
    {
        [Key]
        public int Id { get; set; }
        public DateTime? HorarioChegadaPista { get; set; }
        public string? HorimetroChegadaPista { get; set; }
        public string? CodigoICAOPista { get; set; }
        public string? NomePista { get; set; }
        public string? LatPista { get; set; }
        public string? LongPista { get; set; }

        [ForeignKey("CombateIncendio")]
        public int CombateIncendioId { get; set; }

        [JsonIgnore]
        public virtual CombateIncendio? CombateIncendio { get; set; }
    }
}
