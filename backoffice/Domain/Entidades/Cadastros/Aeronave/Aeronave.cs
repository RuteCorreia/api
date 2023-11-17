using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.Aeronave;

public class Aeronave
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Empresa")]
    public int? IdEmpresa { get; set; }
    public string? Prefixo { get; set; }
    public string? Combustivel { get; set; }
    public int? CapacidadeDeCarga { get; set; }
    public string? Horimetro { get; set; }
    [JsonIgnore]
    public virtual Empresa.Empresa? Empresa { get; set; }
}
