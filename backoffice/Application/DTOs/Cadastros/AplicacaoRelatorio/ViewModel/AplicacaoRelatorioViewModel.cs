using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Cadastros.AplicacaoRelatorio.ViewModel;

public class AplicacaoRelatorioViewModel
{   
    public int Id { get; set; }
    public int? IdAplicacao { get; set; }
    public int? IdPista { get; set; }
    public decimal? Dosagem { get; set; }
    public string KG_LT { get; set; }
    public int? VolumeAplicacao { get; set; }
    public decimal? TotalAreaAplicada { get; set; }
    public string Alteracoes_Observacoes { get; set; }
}
