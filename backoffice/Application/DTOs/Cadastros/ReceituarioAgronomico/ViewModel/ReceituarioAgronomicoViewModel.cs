using Application.DTOs.Cadastros.DataFormat.ViewModel;
using System.Text.Json.Serialization;

namespace Application.DTOs.Cadastros.ReceituarioAgronomico.ViewModel;

public class ReceituarioAgronomicoViewModel
{
    public int? Id { get; set; }

    [JsonPropertyName("relatorioAplicacaoId")]
    public int? RelatorioAplicacaoId { get; set; }

    [JsonPropertyName("nomeArquivo")]
    public DataFormatViewModel NomeArquivo { get; set; }

    [JsonPropertyName("nomeArquivoStr")]
    public string NomeArquivoStr { get; set; }

    [JsonPropertyName("numero")]
    public string Numero { get; set; }

    [JsonPropertyName("dataEmissao")]
    public string DataEmissao { get; set; }
}