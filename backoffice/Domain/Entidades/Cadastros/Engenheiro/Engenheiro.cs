using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.Engenheiro;

public class Engenheiro
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Empresa")]
    public int IdEmpresa { get; set; }

    public string Nome { get; set; }

    public string Email { get; set; }

    public string Senha { get; set; }

    public string CREA { get; set; }
    public string? Telefone { get; set; }

    public int PorcentagemComissao { get; set; }

    public string Assinatura { get; set; }

    [JsonIgnore]
    public virtual Empresa.Empresa? Empresa { get; set; }
}
