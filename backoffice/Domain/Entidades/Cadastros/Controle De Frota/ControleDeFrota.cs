using Domain.Entidades.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.Controle_De_Frota;

public class ControleDeFrota
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("Frota")]
    public int? IdFrota { get; set; }
    [ForeignKey("Veiculo")]
    public int? IdVeiculo { get; set; }
    public string? NomeVeiculo { get; set; }
    public string? Observacao { get; set; }
    public DateTime? DataCriacao { get; set; }
    public DateTime? DataAtualizacao { get; set; }
    public int? KmInicial { get; set; }
    public int? KmFinal { get; set; }
    [ForeignKey("Aeronave")]
    public int? IdAeronave { get; set; }
    public string? NomeAeronave { get; set; }
    public string? HorimetroInicial { get; set; }
    public string? HorimetroFinal { get; set; }
    public decimal? CombustivelInicial { get; set; }
    public decimal? CombustivelFinal { get; set; }
    public decimal? QtdeCombustivel { get; set; }
    [ForeignKey("Piloto")]
    public Guid? IdPiloto { get; set; }
    public string? NomePiloto { get; set; }
    [ForeignKey("Executor")]
    public Guid? IdExecutor { get; set; }
    public string? NomeExecutor { get; set; }
    public DateTime? Data { get; set; }
    public decimal? Extensao { get; set; }
    public string? Combustivel { get; set; }
    public int? LocalInicial { get; set; }
    public string? LocalizacaoPistaLat { get; set; }
    public string? LocalizacaoPistaLon { get; set; }
    public int? QtdeHectare { get; set; }
    [ForeignKey("DataRelatorio")]
    public int? IdData { get; set; }
    [ForeignKey("Empresa")]
    public int? IdEmpresa { get; set; }
    public string? Imagem { get; set; }

    [JsonIgnore]
    public virtual Frota.Frota? Frota { get; set; }
    [JsonIgnore]
    public virtual Veiculo.Veiculo? Veiculo { get; set; }
    [JsonIgnore]
    public virtual Aeronave.Aeronave? Aeronave { get; set; }
    [JsonIgnore]
    public virtual Usuario? Piloto { get; set; }

    [JsonIgnore]
    public virtual Usuario? Executor { get; set; }
    [JsonIgnore]
    public virtual DataRelatorio.DataRelatorio? DataRelatorio { get; set; }
    [JsonIgnore]
    public virtual Empresa.Empresa? Empresa { get; set; }
}
