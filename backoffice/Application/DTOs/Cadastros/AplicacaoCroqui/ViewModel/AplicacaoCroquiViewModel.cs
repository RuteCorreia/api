using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Cadastros.AplicacaoCroqui.ViewModel;

public class AplicacaoCroquiViewModel
{
    public int Id { get; set; }
    public int? IdAplicacao { get; set; }
    public string Desenho { get; set; }
    public int? IdMapa { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
}
