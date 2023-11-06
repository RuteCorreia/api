using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.Alvo_Biologico;

public class AlvoBiologico
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Produto")]
    public int? IdProduto { get; set; }
    public string Nome { get; set; }
    public string DoseProdutoPorHectare { get; set; }
    [JsonIgnore]
    public virtual Produto.Produto? Produto { get; set; }
}
