using Application.DTOs.Cadastros.AplicacaoAreaTratada.ViewModel;
using Application.DTOs.Cadastros.AplicacaoRecomendacoesTecnicas.ViewModel;
using Application.DTOs.Cadastros.AplicacaoRelatorio.ViewModel;
using Application.DTOs.Cadastros.AplicacaoRelatorioItem.ViewModel;
using Application.DTOs.Cadastros.CaracteristicasProdutoAplicado.ViewModel;
using Application.DTOs.Cadastros.Contratante.ViewModel;
using Application.DTOs.Cadastros.ContratoPrestacaoServico.ViewModel;
using Application.DTOs.Cadastros.DadosResponsavel.ViewModel;
using Application.DTOs.Cadastros.ProdutoAplicado.ViewModel;
using Application.DTOs.Cadastros.ReceituarioAgronomico.ViewModel;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Application.DTOs.Cadastros.RelatorioAplicacao.ViewModel
{
    public class RelatorioAplicacaoViewModel
    {
        public int Id { get; set; }
        public bool IsMapa { get; set; }
        public int? ContratanteId { get; set; }
        public int? StatusEnvio { get; set; }
        public string? NomeRelatorio { get; set; }
        public int? IdentificacaoAreaTratadaId { get; set; }
        public int? CaracteristicasProdutoAplicadoId { get; set; }
        public int? RecomendacoesTecnicasId { get; set; }
        public int? AplicacaoRelatorioId { get; set; }
        public int? ContratoPrestacaoServicoId { get; set; }
        public int? DadosResponsavelId { get; set; }
        public int? CulturaId { get; set; }
        public int? PilotoId { get; set; }
        public string? Piloto { get; set; }
        public int? ExecutorId { get; set; }
        public string? Executor { get; set; }
        public int? AuxiliarPistaId { get; set; }
        public bool? IsDrone { get; set; }
        public string? RefDocument { get; set; }
        public string? Data { get; set; }
        public int? IdData { get; set; }
        public DateTime? DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public int? assinaturaType { get; set; }
        public string? userId { get; set; }
        public string? RefUsuario { get; set; }
        public int? State { get; set; }
        public int? IdEmpresa { get; set; }
        public virtual AplicacaoRecomendacoesTecnicasViewModel? AplicacaoRecomendacoesTecnicas { get; set; }
        public virtual ContratanteViewModel? Contratante { get; set; }
        public virtual AreaTratadaViewModel? IdentificacaoAreaTratada { get; set; }
        public virtual ProdutoAplicadoViewModel? CaracteristicasProdutoAplicado { get; set; }
        public virtual ContratoPrestacaoServicoViewModel? ContratoPrestacaoServico { get; set; }
        public virtual DadosResponsavelViewModel? DadosResponsavel { get; set; }
        public virtual AplicacaoRelatorioViewModel? AplicacaoRelatorio { get; set; }
        public virtual IEnumerable<RelatorioItemViewModel>? Aplicacoes{ get; set; }
        public virtual IEnumerable<ProdutoAplicadoCaracteristicasViewModel>? ProdutosAplicados { get; set; }
        public virtual IEnumerable<ReceituarioAgronomicoViewModel>? ReceituariosAgronomicos { get; set; }
    }
}
