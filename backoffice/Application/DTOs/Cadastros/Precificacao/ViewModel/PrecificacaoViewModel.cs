using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Cadastros.Precificacao.ViewModel;

public class PrecificacaoViewModel
{
    public int Id { get; set; }
    public int? IdEmpresa { get; set; }
    public string DistanciaPista { get; set; }
    public decimal? PrecoHA { get; set; }
}
