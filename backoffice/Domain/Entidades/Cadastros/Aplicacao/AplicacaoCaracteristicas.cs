using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.Aplicacao;

public class AplicacaoCaracteristicas
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Aplicacao")]
    public int? IdAplicacao { get; set; }

    [ForeignKey("Produto")]
    public int? IdProduto { get; set; }

    [ForeignKey("Adjuvante")]
    public int? IdAdjuvante { get; set; }
    public string TipoDeServico { get; set; }

    [JsonIgnore]
    public virtual Aplicacao? Aplicacao { get; set; }
    [JsonIgnore]
    public virtual Produto.Produto? Produto { get; set; }
    [JsonIgnore]
    public virtual Adjuvante.Adjuvante? Adjuvante { get; set; }
}
