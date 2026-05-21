using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Cadastros.Classe.ViewModel
{
    public class ClasseCreateViewModel
    {
        [Required(ErrorMessage = "Descrição é obrigatória")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tipo de Serviço é obrigatório")]
        public int IdTipoDeServico { get; set; }
    }
}
