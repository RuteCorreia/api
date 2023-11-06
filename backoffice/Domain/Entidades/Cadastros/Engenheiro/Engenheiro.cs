using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.Engenheiro;

public class Engenheiro
{
    [Key]
    public int IdEngenheiro { get; set; }

    [ForeignKey("Empresa")]
    public int? IdEmpresa { get; set; }

    [Required]
    public string Nome { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [PasswordPropertyText]
    public string Senha { get; set; }

    [Required]
    public string CREA { get; set; }
    public byte[] Assinatura { get; set; }

    [JsonIgnore]
    public virtual Empresa.Empresa? Empresa { get; set; }
}
