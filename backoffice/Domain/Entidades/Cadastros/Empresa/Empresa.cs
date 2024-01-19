using System.ComponentModel.DataAnnotations;

namespace Domain.Entidades.Cadastros.Empresa;

public class Empresa
{
    [Key]
    public int IdEmpresa { get; set; }

    [Required]
    public string Nome { get; set; }

    public byte[] Imagem { get; set; }

    [Required]
    [Display(Name ="Plano Contratado")]
    public int? PlanoContratado { get; set; }

}
