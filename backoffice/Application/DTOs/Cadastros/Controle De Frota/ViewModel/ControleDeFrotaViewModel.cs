using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.DTOs.Cadastros.Controle_De_Frota.ViewModel;

public class ControleDeFrotaViewModel
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("idVeiculo")]
    public int? IdVeiculo { get; set; }

    [JsonProperty("placaVeiculo")]
    public string? NomeVeiculo { get; set; }

    [JsonProperty("observacao")]
    public string? Observacao { get; set; }

    [JsonProperty("createdAt")]
    public DateTime? DataCriacao { get; set; }

    [JsonProperty("updateDate")]
    public DateTime? DataAtualizacao { get; set; }

    [JsonProperty("kminicial")]
    public int? KmInicial { get; set; }

    [JsonProperty("kmfinal")]
    public int? KmFinal { get; set; }

    [JsonProperty("idAeronave")]
    public int? IdAeronave { get; set; }

    [JsonProperty("aeronave")]
    public string? NomeAeronave { get; set; }

    [JsonProperty("horimetroInicial")]
    public string? HorimetroInicial { get; set; }

    [JsonProperty("horimetroFinal")]
    public string? HorimetroFinal { get; set; }

    [JsonProperty("combustivelInicial")]
    public decimal? CombustivelInicial { get; set; }

    [JsonProperty("combustivelFinal")]
    public decimal? CombustivelFinal { get; set; }

    [JsonProperty("totalLitros")]
    public decimal? QtdeCombustivel { get; set; }

    [JsonProperty("idPiloto")]
    public Guid? IdPiloto { get; set; }

    [JsonProperty("piloto")]
    public string? NomePiloto { get; set; }

    [JsonProperty("idExecutor")]
    public Guid? IdExecutor { get; set; }

    [JsonProperty("executor")]
    public string? NomeExecutor { get; set; }

    [JsonProperty("date")]
    public DateTime? Data { get; set; }

    [JsonProperty("extensao")]
    public decimal? Extensao { get; set; }

    [JsonProperty("combustivel")]
    public string? Combustivel { get; set; }

    [JsonProperty("data_id")]
    public int? IdData { get; set; }

    [JsonProperty("imageData")]
    public string? Imagem { get; set; }
    public string? NomeRelatorio { get; set; }
    public int? State { get; set; }
    public bool? IsDrone { get; set; }

}
