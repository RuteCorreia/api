using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Cadastros.AplicacaoRecomendacoesTecnicas.ViewModel;

public class AplicacaoRecomendacoesTecnicasViewModel
{
    public int Id { get; set; }
    public int? IdAplicacao { get; set; }
    public int? IdVeiculante { get; set; }
    public int? QtdeVeiculante { get; set; }
    public int? LarguraFaixa { get; set; }
    public int? VolumeAplicacao { get; set; }
    public int? IdAeronave { get; set; }
    public int? IdAlturaVoo { get; set; }
    public string AlturaVooCustom { get; set; }
    public string Temperatura { get; set; }
    public int? UrDoAR { get; set; }
    public string VelocidadeVento { get; set; }
    public int? IdTipoDeProduto { get; set; }
    public int? IdEquipamento { get; set; }
    public string Angulo { get; set; }
}
