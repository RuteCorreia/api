using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.ProdutoAplicado;

public class ProdutoAplicado
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("CaracteristicasProdutoAplicado")]
    public int IdCaracteristicasProdutoAplicado { get; set; }
    public string TipoServico { get; set; }
    public string NomeProduto { get; set; }
    public string AlvoBiologico { get; set; }
    public int? ClassificacaoToxicologica { get; set; }
    public string Classe { get; set; }
    public string DoseProdutoHectare { get; set; }
    public string UnidadeDoseProdutoHectare { get; set; }
}
