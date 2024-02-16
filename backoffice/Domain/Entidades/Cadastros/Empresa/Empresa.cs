using System.ComponentModel.DataAnnotations;

namespace Domain.Entidades.Cadastros.Empresa;

public class Empresa
{
    [Key]
    public int IdEmpresa { get; set; }

    [Required]
    public string Nome { get; set; }

    [Required]
    public string Email { get; set; }
    public string? Telefone { get; set; }
    public string? RegistroMapa { get; set; }

    [Required]
    public string CNPJ { get; set; }
    public string? InscricaoEstadual { get; set; }
    public int? NrCDA { get; set; }
    public string? Endereco { get; set; }
    public string? Numero { get; set; }

    [Required]
    public string Estado { get; set; }
        
    [Required]
    public string Cidade { get; set; }

    public byte[] Imagem { get; set; }


}
