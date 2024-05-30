using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.ManutencaoAeronave;

public class ManutencaoAeronave
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Aeronave")]
    public int? IdAeronave { get; set; }
    public string? Horimetro { get; set; }
    public byte[]? Documento { get; set; }
    public byte[]? FichaInspecao { get; set; }
    public byte[]? ManualAeronave { get; set; }
    public byte[]? MapaComponentes { get; set; }

    [ForeignKey("Empresa")]
    public int? IdEmpresa { get; set; }

    [JsonIgnore]
    public virtual Empresa.Empresa? Empresa { get; set; }

    [JsonIgnore]
    public virtual Aeronave.Aeronave? Aeronave { get; set; }
}
