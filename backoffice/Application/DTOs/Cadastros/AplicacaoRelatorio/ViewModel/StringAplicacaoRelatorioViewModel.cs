using Application.DTOs.Cadastros.AplicacaoRelatorioItem.ViewModel;
using System.Text.Json.Serialization;

namespace Application.DTOs.Cadastros.AplicacaoRelatorio.ViewModel
{
    public class StringAplicacaoRelatorioViewModel
    {
        public int Id { get; set; }
        public int? IdAplicacao { get; set; }

        public int? IdPista { get; set; }

        [JsonPropertyName("dosagem")]
        public decimal? Dosagem { get; set; }

        [JsonPropertyName("unidadeDosagem")]
        public string UnidadeDosagem { get; set; }

        [JsonPropertyName("volumeAplicacao")]
        public int? VolumeAplicacao { get; set; }

        [JsonPropertyName("totalAreaAplicada")]
        public decimal? TotalAreaAplicada { get; set; }

        [JsonPropertyName("observacoes")]
        public string? Observacoes { get; set; }

        [JsonPropertyName("cultura")]
        public string? Cultura { get; set; }

        [JsonPropertyName("produtoAplicado")]
        public string? ProdutoAplicado { get; set; }

        [JsonPropertyName("localizacaoPistaCodigoICAO")]
        public string? LocalizacaoPistaCodigoICAO { get; set; }

        [JsonPropertyName("lat")]
        public string? Lat { get; set; }

        [JsonPropertyName("long")]
        public string? Long { get; set; }

        [JsonPropertyName("densidade")]
        public string? Densidade { get; set; }

        [JsonPropertyName("relatorioDGPS")]
        public string? RelatorioDGPS { get; set; }

        [JsonPropertyName("unidadeVolumeAplicacao")]
        public string? UnidadeVolumeAplicacao { get; set; }

        [JsonPropertyName("mapaAplicacao")]
        public List<string>? MapaAplicacao { get; set; }

        [JsonPropertyName("aplicacoes")]
        public List<RelatorioItemViewModel>? Aplicacoes { get; set; }
    }
}
