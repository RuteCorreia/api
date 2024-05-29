using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Cadastros.Equipamento.ViewModel;

public class EquipamentoViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [MinLength(3)]
    [MaxLength(120)]
    public string Nome { get; set; }
}
