using Newtonsoft.Json;

namespace Application.DTOs.Cadastros.Atividade.ViewModel
{
    public class AtividadeFiltroViewModel
    {
        [JsonProperty("aeronave")]
        public string? PrefixoAeronave { get; set; }

        [JsonProperty("piloto")]
        public string? Piloto { get; set; }

        [JsonProperty("executor")]
        public string? Executor { get; set; }

        [JsonProperty("cliente")]
        public string? Contratante { get; set; }

        [JsonProperty("dataInicio")]
        public DateTime? DataInicial { get; set; }

        [JsonProperty("dataFim")]
        public DateTime? DataFinal { get; set; }
    }
}
