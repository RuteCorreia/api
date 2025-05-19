using System.Text.Json.Serialization;

namespace Application.DTOs.Cadastros.ProdutoAplicado.ViewModel;

public class ProdutoAplicadoCaracteristicasViewModel
{
    public int? Id { get; set; }

    [JsonPropertyName("relatorioAplicacaoId")]
    public int? RelatorioAplicacaoId { get; set; }

    [JsonPropertyName("tipoServico")]
    public string TipoServico { get; set; }

    [JsonPropertyName("nomeProduto")]
    public string NomeProduto { get; set; }

    [JsonPropertyName("alvoBiologico")]
    public string AlvoBiologico { get; set; }

    [JsonPropertyName("classificacaoToxicologica")]
    public int? ClassificacaoToxicologica { get; set; }

    [JsonPropertyName("classe")]
    public string Classe { get; set; }

    [JsonPropertyName("doseProdutoHectare")]
    public string DoseProdutoHectare { get; set; }

    [JsonPropertyName("unidadeDoseProdutoHectare")]
    public string UnidadeDoseProdutoHectare { get; set; }

    [JsonPropertyName("dosagemProdutoAplicado")]
    public decimal? DosagemProdutoAplicado { get; set; }
    
    [JsonPropertyName("unidadeProdutoAplicado")]
    public string UnidadeProdutoAplicado { get; set; }
}