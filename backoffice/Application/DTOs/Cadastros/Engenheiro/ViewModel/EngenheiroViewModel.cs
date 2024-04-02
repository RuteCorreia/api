using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Cadastros.Engenheiro.ViewModel;

public class EngenheiroViewModel
{
    public string Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [MinLength(3)]
    [MaxLength(120)]
    public string Nome { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    public string? Telefone { get; set; }
}
