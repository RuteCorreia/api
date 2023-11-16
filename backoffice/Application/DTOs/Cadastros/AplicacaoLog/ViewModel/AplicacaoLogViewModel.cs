using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Cadastros.AplicacaoLog.ViewModel;

public class AplicacaoLogViewModel
{
    public int Id { get; set; }
    public int? IdAplicacao { get; set; }
    public DateTime? Data { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [MinLength(3)]
    [MaxLength(120)]
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
}
