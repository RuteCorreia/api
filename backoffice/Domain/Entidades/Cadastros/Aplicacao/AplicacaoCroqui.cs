using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.Aplicacao;

public class AplicacaoCroqui
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Aplicacao")]
    public int? IdAplicacao { get; set; }
    public string Desenho { get; set; }
    public int? IdMapa { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }

    [JsonIgnore]
    public virtual Aplicacao? Aplicacao { get; set; }
}
