using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.Aplicacao;

public class AplicacaoContrato
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Aplicacao")]
    public int? IdAplicacao { get; set; }

    [ForeignKey("Estado")]
    public int? IdUF { get; set; }

    [ForeignKey("Cidade")]
    public int? IdCidade { get; set; }
    public string? NomeCliente { get; set; }
    public string? CPFCliente { get; set; }
    public string? Assinatura { get; set; }

    [JsonIgnore]
    public virtual Aplicacao? Aplicacao { get; set; }
    [JsonIgnore]
    public virtual Estados.Estados? Estado { get; set; }
    [JsonIgnore]
    public virtual Cidades.Cidades? Cidade { get; set; }
}
