using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Cadastros.Pistas.ViewModel;

public class PistaViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [MinLength(3)]
    [MaxLength(120)]
    public string Nome { get; set; }
    public string LAT { get; set; }
    public string LONG { get; set; }
    public int? IdEmpresa { get; set; }
}
