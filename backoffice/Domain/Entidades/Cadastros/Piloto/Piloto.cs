using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.Piloto;

public class Piloto
{
    [Key]
    public int IdPiloto { get; set; }

    [ForeignKey("Empresa")]
    public int? IdEmpresa { get; set; }

    [Required]
    public string NomePiloto { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [PasswordPropertyText]
    public string Senha { get; set; }

    [Required]
    public string CDAC { get; set; }

    [Required]
    public byte[] Assinatura { get; set; }

    [Required]
    public string PorcentagemComissao { get; set; }

    [JsonIgnore]
    public virtual Empresa.Empresa? Empresa { get; set; }
}
