using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Cadastros.Tipo_Produto.ViewModel;

public class TipoProdutoViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [MinLength(3)]
    [MaxLength(120)]
    public string Nome { get; set; }
}
