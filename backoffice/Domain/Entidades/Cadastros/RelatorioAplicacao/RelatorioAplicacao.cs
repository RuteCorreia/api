using Domain.Entidades.Cadastros.Aplicacao;
using Domain.Entidades.Cadastros.CaracteristicasProdutoAplicado;
using Domain.Entidades.Cadastros.Contratante;
using Domain.Entidades.Cadastros.DadosResponsavel;
using Domain.Entidades.Cadastros.IdentificacaoAreaTratada;
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

        [ForeignKey("Contratante")]
        public int? ContratanteId { get; set; }

        [ForeignKey("IdentificacaoAreaTratada")]
        public int? IdentificacaoAreaTratadaId { get; set; }

        [ForeignKey("CaracteristicasProdutoAplicado")]
        public int? CaracteristicasProdutoAplicadoId { get; set; }

        [ForeignKey("AplicacaoRecomendacoesTecnicas")]
        public int? RecomendacoesTecnicasId { get; set; }
        public int? RelatorioAplicacaoId { get; set; }

        [ForeignKey("ContratoPrestacaoServico")]
        public int? ContratoPrestacaoServicoId { get; set; }

        [ForeignKey("DadosResponsavel")]
        public int? DadosResponsavelId { get; set; }
        public int? ClienteId { get; set; }
        public int? CulturaId { get; set; }
        public int? PilotoId { get; set; }
        public string? Piloto { get; set; }
        public int? ExecutorId { get; set; }
        public string? Executor { get; set; }

        [ForeignKey("AuxiliarPista")]
        public int? AuxiliarPistaId { get; set; }
        public bool? IsDrone { get; set; }
        public string RefDocument { get; set; }
        public string Data { get; set; }
        public DateTime? DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public string RefUsuario { get; set; }

        [JsonIgnore]
        public virtual AplicacaoRecomendacoesTecnicas? AplicacaoRecomendacoesTecnicas { get; set; }

        [JsonIgnore]
        public virtual Contratante.Contratante? Contratante { get; set; }

        [JsonIgnore]
        public virtual IdentificacaoAreaTratada.IdentificacaoAreaTratada? IdentificacaoAreaTratada { get; set; }

        [JsonIgnore]
        public virtual CaracteristicasProdutoAplicado.CaracteristicasProdutoAplicado? CaracteristicasProdutoAplicado { get; set; }

        [JsonIgnore]
        public virtual ContratoPrestacaoServico.ContratoPrestacaoServico? ContratoPrestacaoServico { get; set; }

        [JsonIgnore]
        public virtual DadosResponsavel.DadosResponsavel? DadosResponsavel { get; set; }
        [JsonIgnore]
        public virtual AuxiliarPista.AuxiliarPista? AuxiliarPista { get; set; }
    }
}
