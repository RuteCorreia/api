using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Cadastros.Executor.ViewModel;

public class ExecutorViewModel
{
    public string Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [MinLength(3)]
    [MaxLength(120)]
    public string Nome { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    public string? Telefone { get; set; }

    public string? Assinatura { get; set; }

    public string? CFTA { get; set; }
}
