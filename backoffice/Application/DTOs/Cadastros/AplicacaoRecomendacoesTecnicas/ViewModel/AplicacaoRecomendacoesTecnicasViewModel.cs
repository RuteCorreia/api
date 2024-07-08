using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Application.DTOs.Cadastros.AplicacaoRecomendacoesTecnicas.ViewModel;

public class AplicacaoRecomendacoesTecnicasViewModel
{
    public int Id { get; set; }

    [JsonProperty("veiculante")]
    public string? NomeVeiculante { get; set; }

    [JsonProperty("qtdVeiculante")]
    public int? QtdVeiculante { get; set; }

    [JsonProperty("larguraFaixa")]
    public int? LarguraFaixa { get; set; }

    [JsonProperty("volumeAplicacao")]
    public int? VolumeAplicacao { get; set; }

    [JsonProperty("unidadeVolumeAplicacao")]
    public string? UnidadeVolumeAplicacao { get; set; }

    [JsonProperty("aeronave")]
    public string? NomeAeronave { get; set; }

    [JsonProperty("alturaVoo")]
    public string? QtdAlturaVoo { get; set; }

    [JsonProperty("temperatura")]
    public string? Temperatura { get; set; }

    [JsonProperty("umidadeRelativaAr")]
    public string? UmidadeRelativaAr { get; set; }

    [JsonProperty("velocidadeVento")]
    public string? VelocidadeVento { get; set; }

    [JsonProperty("tipoProduto")]
    public string? TipoDeProduto { get; set; }

    [JsonProperty("equipamento")]
    public string? NomeEquipamento { get; set; }

    [JsonProperty("angulo")]
    public string? Angulo { get; set; }

    [JsonProperty("arquiv")]
    public string? ArquivoDrone { get; set; }
    //[JsonIgnore]
    //public virtual Veiculante.Veiculante? Veiculante { get; set; }
    //[JsonIgnore]
    //public virtual Aeronave.Aeronave? Aeronave { get; set; }
    //[JsonIgnore]
    //public virtual AlturaVoo? AlturaVoo { get; set; }
    //[JsonIgnore]
    //public virtual TipoProduto? TipoProduto { get; set; }
    //[JsonIgnore]
    //public virtual Equipamento.Equipamento? Equipamento { get; set; }
}
