using Application.DTOs.Cadastros.DataFormat.ViewModel;
using System.Text.Json.Serialization;

namespace Application.DTOs.Cadastros.CaracteristicasProdutoAplicado.ViewModel;

public class CaracteristicasProdutoAplicadoViewModel
{
    public int? Id { get; set; }

    [JsonPropertyName("cultura")]
    public string Cultura { get; set; }

    [JsonPropertyName("receituarioAgronomico")]
    public DataFormatViewModel ReceiturarioAgronomico { get; set; }

    [JsonPropertyName("nomeProduto")]
    public string NomeProduto { get; set; }

    [JsonPropertyName("classificacaoToxicologica")]
    public int? ClassificacaoToxicologica { get; set; }

    [JsonPropertyName("classe")]
    public string Classe { get; set; }

    [JsonPropertyName("tipoFormulacao")]
    public string TipoFormulacao { get; set; }

    [JsonPropertyName("alvoBiologico")]
    public string AlvoBiologico { get; set; }

    [JsonPropertyName("doseProdutoHectare")]
    public string DoseProdutoHectare { get; set; }

    [JsonPropertyName("unidadeDoseProdutoHectare")]
    public string UnidadeDoseProdutoHectare { get; set; }

    [JsonPropertyName("adjuvante")]
    public string Adjuvante { get; set; }

    [JsonPropertyName("tipoServico")]
    public string TipoServico { get; set; }

    [JsonPropertyName("numeroReceituarioAgronomico")]
    public string NumeroReceituarioAgronomico { get; set; }

    [JsonPropertyName("dataEmissao")]
    public string DataEmissao { get; set; }

    [JsonPropertyName("isReceituarioImage")]
    public bool? IsReceituarioImage { get; set; }
}