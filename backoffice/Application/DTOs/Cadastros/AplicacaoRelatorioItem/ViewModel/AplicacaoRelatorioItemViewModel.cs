using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Cadastros.AplicacaoRelatorioItem.ViewModel;

public class AplicacaoRelatorioItemViewModel
{
    public int Id { get; set; }
    public int? IdAplicacaoRelatorio { get; set; }
    public string? HoraInicio { get; set; }
    public string? HorimetroInicial { get; set; }
    public string? HoraTermino { get; set; }
    public string? HorimetroTermino { get; set; }
    public string? TemperaturaInicial { get; set; }
    public string? TemperaturaFinal { get; set; }
    public string? UrInicial { get; set; }
    public string? UrFinal { get; set; }
    public string? VentoInicial { get; set; }
    public string? VentoFinal { get; set; }
    public string? ImagemDadosClimaticos { get; set; }
}
