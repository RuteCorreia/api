using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Cadastros.Engenheiro.ViewModel;

public class EngenheiroViewModel
{
    public int IdEngenheiro { get; set; }
    public int? IdEmpresa { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [MinLength(3)]
    [MaxLength(120)]
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public string CREA { get; set; }
    public byte[] Assinatura { get; set; }
}
