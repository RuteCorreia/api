using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.Aplicacao;

public class AplicacaoLog
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Aplicacao")]
    public int? IdAplicacao { get; set; }
    public DateTime? Data { get; set; }
    public string? Nome { get; set; }
    public string? Descricao { get; set; }

    [JsonIgnore]
    public virtual Aplicacao? Aplicacao { get; set; }
}
