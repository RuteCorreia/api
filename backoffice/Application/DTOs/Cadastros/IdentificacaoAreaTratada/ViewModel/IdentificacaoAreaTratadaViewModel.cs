using Application.DTOs.Cadastros.DataFormat.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs.Cadastros.IdentificacaoAreaTratada.ViewModel
{
    public class IdentificacaoAreaTratadaViewModel
    {
        public int? Id { get; set; }

        [JsonPropertyName("uf")]
        public string UF { get; set; }

        [JsonPropertyName("cidade")]
        public string Cidade { get; set; }

        [JsonPropertyName("localizacao")]
        public string Localizacao { get; set; }

        [JsonPropertyName("cultura")]
        public string Cultura { get; set; }

        [JsonPropertyName("extensao")]
        public string Extensao { get; set; }

        [JsonPropertyName("croquiArea")]
        public DataFormatViewModel CroquiArea { get; set; }
    }
}
