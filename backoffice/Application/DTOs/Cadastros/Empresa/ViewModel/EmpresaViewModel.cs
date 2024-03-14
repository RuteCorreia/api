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
    public string? ImagemBase64 { get; set; }

    [Required]
    public string Email { get; set; }
    public string? Telefone { get; set; }
    public string? RegistroMapa { get; set; }

    [Required]
    public string CNPJ { get; set; }
    public string? InscricaoEstadual { get; set; }
    public int? NrCDA { get; set; }
    public string? CEP { get; set; }
    public string? Endereco { get; set; }
    public string? Numero { get; set; }

    [Required]
    public string Estado { get; set; }

    [Required]
    public string Cidade { get; set; }
}
