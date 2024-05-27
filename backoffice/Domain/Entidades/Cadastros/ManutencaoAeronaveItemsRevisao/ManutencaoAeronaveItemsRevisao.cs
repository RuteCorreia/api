using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.ManutencaoAeronaveItemsRevisao;

public class ManutencaoAeronaveItemsRevisao
{
    [Key]
    public int Id { get; set; }

    public string Descricao { get; set; }

    public bool Revisado { get; set; }

    [ForeignKey("ManutencaoAeronave")]
    public int? IdManutencaoAeronave { get; set; }

    [JsonIgnore]
    public virtual ManutencaoAeronave.ManutencaoAeronave? ManutencaoAeronave { get; set; }
    
}
