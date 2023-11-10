using Domain.Entidades.Cadastros.Produto;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Cadastros.AlvoBiologico.ViewModel;

public class AlvoBiologicoViewModel
{
    public int Id { get; set; }
    public int? IdProduto { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [MinLength(3)]
    [MaxLength(120)]
    public string Nome { get; set; }
    public string DoseProdutoPorHectare { get; set; }
}
