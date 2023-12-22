using Domain.Entidades.Cadastros.Piloto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.Controle_De_Frota;

public class ControleDeFrota
{
    [Key]
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

    [JsonIgnore]
    public virtual Frota.Frota? Frota { get; set; }
    [JsonIgnore]
    public virtual Aeronave.Aeronave? Aeronave { get; set; }
    [JsonIgnore]
    public virtual Piloto.Piloto? Piloto { get; set; }
}
