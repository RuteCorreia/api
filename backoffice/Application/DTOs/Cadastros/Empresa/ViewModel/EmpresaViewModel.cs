using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Cadastros.Empresa.ViewModel;

public class EmpresaViewModel
{
    public int IdEmpresa { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [MinLength(3)]
    [MaxLength(120)]
    public string Nome { get; set; }
    public byte[] Imagem { get; set; }
    public int? PlanoContratado { get; set; }
}
