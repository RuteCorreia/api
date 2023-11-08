using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Cadastros.Cidades.ViewModel;

public class CidadeViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [MinLength(3)]
    [MaxLength(120)]
    public string Nome { get; set; }
    public string Sigla { get; set; }

}
