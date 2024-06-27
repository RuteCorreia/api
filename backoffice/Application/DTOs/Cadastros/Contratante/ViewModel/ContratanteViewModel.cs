using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs.Cadastros.Contratante.ViewModel
{
    public class ContratanteViewModel
    {
        public int Id { get; set; }

        [JsonPropertyName("tipoContratante")]
        public string TipoContratante { get; set; }

        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [JsonPropertyName("cpf")]
        public string CPF { get; set; }

        [JsonPropertyName("endereco")]
        public string Endereco { get; set; }

        [JsonPropertyName("rg")]
        public string RG { get; set; }

        [JsonPropertyName("uf")]
        public string UF { get; set; }

        [JsonPropertyName("cidade")]
        public string Cidade { get; set; }

        [JsonPropertyName("cnpj")]
        public string CNPJ { get; set; }

        [JsonPropertyName("inscricaoEstadual")]
        public string InscricaoEstadual { get; set; }

        [JsonPropertyName("contratanteRef")]
        public string ContratanteRef { get; set; }
    }
}
