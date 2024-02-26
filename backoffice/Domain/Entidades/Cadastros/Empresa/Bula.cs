using Domain.Entidades.Cadastros.Alvo_Biologico;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.Empresa;

public class Bula
{
    [Key]
    public int IdBula { get; set; }
    public string NomeProduto { get; set; }

    [ForeignKey("Cultura")]
    public int? IdCultura { get; set; }

    public int? IdClassificacaoToxicologica { get; set; }
    public string Classe { get; set; }
    public string TipoDeFormulacao { get; set; }

    [ForeignKey("AlvoBiologico")]
    public int? IdAlvoBiologico { get; set; }
    public string? DoseProdutoComercial { get; set; }
    public string Adjuvante { get; set; }
    public int? IdTipoDeServico { get; set; }
    public bool Removido { get; set; }

    [JsonIgnore]
    public virtual Cultura.Cultura? Cultura { get; set; }
    [JsonIgnore]
    public virtual AlvoBiologico? AlvoBiologico { get; set; }
}
