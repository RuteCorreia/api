using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Cadastros.Classe.ViewModel
{
    public class ClasseUpdateViewModel
    {
        [Required(ErrorMessage = "Descrição é obrigatória")]
        public string Descricao { get; set; } = string.Empty;
    }
}
