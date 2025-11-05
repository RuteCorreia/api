using Domain.Entidades.Cadastros.Aplicacao;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.RelatorioAplicacao
{
    public class RelatorioAplicacao
    {
        [Key]
        public int Id { get; set; }

        public bool IsMapa { get; set; }

        [ForeignKey("Contratante")]
        public int? ContratanteId { get; set; }
        public string? NomeRelatorio { get; set; }

        [ForeignKey("IdentificacaoAreaTratada")]
        public int? IdentificacaoAreaTratadaId { get; set; }

        [ForeignKey("CaracteristicasProdutoAplicado")]
        public int? CaracteristicasProdutoAplicadoId { get; set; }

        [ForeignKey("AplicacaoRecomendacoesTecnicas")]
        public int? RecomendacoesTecnicasId { get; set; }

        [ForeignKey("AplicacaoRelatorio")]
        public int? AplicacaoRelatorioId { get; set; }

        [ForeignKey("ContratoPrestacaoServico")]
        public int? ContratoPrestacaoServicoId { get; set; }

        [ForeignKey("DadosResponsavel")]
        public int? DadosResponsavelId { get; set; }
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

        [ForeignKey("DataRelatorio")]
        public int? IdData { get; set; }
        public DateTime? DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public string? RefUsuario { get; set; }
        public int? StatusEnvio { get; set; }
        public string? MapaAplicacao { get; set; }

        public virtual AplicacaoRecomendacoesTecnicas? AplicacaoRecomendacoesTecnicas { get; set; }
        public virtual Contratante.Contratante? Contratante { get; set; }
        public virtual IdentificacaoAreaTratada.IdentificacaoAreaTratada? IdentificacaoAreaTratada { get; set; }
        public virtual CaracteristicasProdutoAplicado.CaracteristicasProdutoAplicado? CaracteristicasProdutoAplicado { get; set; }
        public virtual ContratoPrestacaoServico.ContratoPrestacaoServico? ContratoPrestacaoServico { get; set; }
        public virtual DadosResponsavel.DadosResponsavel? DadosResponsavel { get; set; }
        public virtual AplicacaoRelatorio? AplicacaoRelatorio { get; set; }
        public virtual IEnumerable<AplicacaoRelatorio>? Aplicacoes { get; set; }

        [ForeignKey("Empresa")]
        public int? IdEmpresa { get; set; }

        [JsonIgnore]
        public virtual Empresa.Empresa? Empresa { get; set; }

        [JsonIgnore]
        public virtual AuxiliarPista.AuxiliarPista? AuxiliarPista { get; set; }

        [JsonIgnore]
        public virtual DataRelatorio.DataRelatorio? DataRelatorio { get; set; }

        public int? assinaturaType { get; set; }
        public string? userId { get; set; }
    }
}
