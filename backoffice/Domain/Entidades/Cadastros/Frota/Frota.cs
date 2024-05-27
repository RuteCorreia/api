using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.Frota;

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

    [JsonIgnore]
    public virtual Empresa.Empresa? Empresa { get; set; }
}
