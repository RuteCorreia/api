using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Application.DTOs.Cadastros.AplicacaoRecomendacoesTecnicas.ViewModel;

public class AplicacaoRecomendacoesTecnicasViewModel
{
    public int Id { get; set; }

    [JsonPropertyName("veiculante")]
    public string? Veiculante { get; set; }

    [JsonPropertyName("qtdVeiculante")]
    public int? QtdVeiculante { get; set; }

    [JsonPropertyName("larguraFaixa")]
    public int? LarguraFaixa { get; set; }

    [JsonPropertyName("volumeAplicacao")]
    public int? VolumeAplicacao { get; set; }

    [JsonPropertyName("unidadevolumeAplicacao")]
    public string? UnidadeVolumeAplicacao { get; set; }

    [JsonPropertyName("aeronave")]
    public string? Aeronave { get; set; }

    [JsonPropertyName("alturaVoo")]
    public string? AlturaVoo { get; set; }

    [JsonPropertyName("temperatura")]
    public string? Temperatura { get; set; }

    [JsonPropertyName("umidadeRelativaAr")]
    public string? UmidadeRelativaAr { get; set; }

    [JsonPropertyName("velocidadeVento")]
    public string? VelocidadeVento { get; set; }

    [JsonPropertyName("tipoProduto")]
    public string? TipoProduto { get; set; }

    [JsonPropertyName("equipamento")]
    public string? Equipamento { get; set; }

    [JsonPropertyName("angulo")]
    public string? Angulo { get; set; }

    [JsonPropertyName("arquivoDrone")]
    public string? ArquivoDrone { get; set; }
}
