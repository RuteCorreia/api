using Domain.Entidades.Cadastros.Altura_Voo;
using Domain.Entidades.Cadastros.Tipo_Produto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.Aplicacao;

public class AplicacaoRecomendacoesTecnicas
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Aplicacao")]
    public int? IdAplicacao { get; set; }

    [ForeignKey("Veiculante")]
    public int? IdVeiculante { get; set; }
    public string? Veinculante { get; set; }
    public int? QtdeVeiculante { get; set; }
    public int? LarguraFaixa { get; set; }
    public int? VolumeAplicacao { get; set; }
    public string? UnidadeVolumeAplicacao { get; set; }

    [ForeignKey("Aeronave")]
    public int? IdAeronave { get; set; }
    public string? NomeAeronave { get; set; }

    [ForeignKey("AlturaVoo")]
    public int? IdAlturaVoo { get; set; }
    public string? AlturaVooCustom { get; set; }
    public string? Temperatura { get; set; }
    public string? UrDoAR { get; set; }
    public string? VelocidadeVento { get; set; }

    [ForeignKey("TipoProduto")]
    public int? IdTipoDeProduto { get; set; }
    public string? TipoDeProduto { get; set; }

    [ForeignKey("Equipamento")]
    public int? IdEquipamento { get; set; }
    public string? NomeEquipamento { get; set; }
    public string? Angulo { get; set; }
    public string? ArquivoDrone { get; set; }

    [ForeignKey("Empresa")]
    public int? IdEmpresa { get; set; }

    [JsonIgnore]
    public virtual Empresa.Empresa? Empresa { get; set; }

    [JsonIgnore]
    public virtual Aplicacao? Aplicacao { get; set; }
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
