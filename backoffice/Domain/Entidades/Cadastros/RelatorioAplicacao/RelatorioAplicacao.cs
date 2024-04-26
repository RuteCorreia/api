using Domain.Entidades.Cadastros.Aplicacao;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entidades.Cadastros.RelatorioAplicacao
{
    public class RelatorioAplicacao
    {
        public int Id { get; set; }
        public int ContratanteId { get; set; }
        public int IdentificacaoAreaTratadaId { get; set; }
        public int CaracteristicasProdutoAplicadoId { get; set; }
        public int RecomendacoesTecnicasId { get; set; }

        [ForeignKey("AplicacaoRecomendacoesTecnicas")]
        public int RelatorioAplicacaoId { get; set; }
        public int ContratoPrestacaoServicoId { get; set; }
        public int DadosResponsavelId { get; set; }
        public string Piloto { get; set; }
        public string Executor { get; set; }
        public string RefDocument { get; set; }
        public string Data { get; set; }
        public string RefUsuario { get; set; }

        [JsonIgnore]
        public virtual AplicacaoRecomendacoesTecnicas? AplicacaoRecomendacoesTecnicas { get; set; }
    }
}
