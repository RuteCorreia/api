using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.Aplicacao;

public class AplicacaoCroquiImportacao
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("AplicacaoCroqui")]
    public int? IdAplicacaoCroqui { get; set; }
    public string Arquivo { get; set; }

    [JsonIgnore]
    public virtual AplicacaoCroqui? AplicacaoCroqui { get; set; }
}
