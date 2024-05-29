using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.Produto;

public class Produto
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Cultura")]
    public int? IdCultura { get; set; }
    public string Nome { get; set; }
    public string ClassificacaoToxicologica { get; set; }
    public string Classe { get; set; }
    public string TipoDeFormulacao { get; set; }
    public string TipoServico { get; set; }
    [JsonIgnore]
    public virtual Cultura.Cultura? Cultura { get; set; }
}
