using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Cadastros.AplicacaoAreaTratada.ViewModel;

public class AplicacaoAreaTratadaViewModel
{
    public int Id { get; set; }
    public int? IdAplicacao { get; set; }
    public int? IdEstado { get; set; }
    public int? IdCidade { get; set; }
    public string Localizacao { get; set; }
    public decimal? Extensao { get; set; }
}
