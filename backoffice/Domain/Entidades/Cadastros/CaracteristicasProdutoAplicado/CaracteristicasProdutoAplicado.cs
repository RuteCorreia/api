using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.CaracteristicasProdutoAplicado;

public class CaracteristicasProdutoAplicado
{
    [Key]
    public int Id { get; set; }
    public string Cultura { get; set; }
    public string ReceiturarioAgronomico { get; set; }
    public string NomeProduto { get; set; }
    public int? ClassificacaoToxicologica { get; set; }
    public string Classe { get; set; }
    public string TipoFormulacao { get; set; }
    public string AlvoBiologico { get; set; }
    public string DoseProdutoHectare { get; set; }
    public string UnidadeDoseProdutoHectare { get; set; }
    public string Adjuvante { get; set; }
    public string TipoServico { get; set; }
    public string NumeroReceituarioAgronomico { get; set; }
    public string DataEmissao { get; set; }
    public bool? IsReceituarioImage { get; set; }

    [ForeignKey("Empresa")]
    public int? IdEmpresa { get; set; }

    [JsonIgnore]
    public virtual Empresa.Empresa? Empresa { get; set; }
}
