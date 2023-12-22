using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.Precificacao;

public class Precificacao
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Empresa")]
    public int? IdEmpresa { get; set; }
    public string DistanciaPista { get; set; }
    public decimal? PrecoHA { get; set; }

    [JsonIgnore]
    public virtual Empresa.Empresa? Empresa { get; set; }

}
