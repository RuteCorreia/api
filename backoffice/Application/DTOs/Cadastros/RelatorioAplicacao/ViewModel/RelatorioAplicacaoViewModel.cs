using Application.DTOs.Cadastros.AplicacaoRecomendacoesTecnicas.ViewModel;
using Application.DTOs.Cadastros.CaracteristicasProdutoAplicado.ViewModel;
using Application.DTOs.Cadastros.Contratante.ViewModel;
using Application.DTOs.Cadastros.ContratoPrestacaoServico.ViewModel;
using Application.DTOs.Cadastros.DadosResponsavel.ViewModel;
using Application.DTOs.Cadastros.IdentificacaoAreaTratada.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Cadastros.RelatorioAplicacao.ViewModel
{
    public class RelatorioAplicacaoViewModel
    {
        public int Id { get; set; }
        public int? ContratanteId { get; set; }
        public int? IdentificacaoAreaTratadaId { get; set; }
        public int? CaracteristicasProdutoAplicadoId { get; set; }
        public int? RecomendacoesTecnicasId { get; set; }
        public int? RelatorioAplicacaoId { get; set; }
        public int? ContratoPrestacaoServicoId { get; set; }
        public int? DadosResponsavelId { get; set; }
        public string Piloto { get; set; }
        public string Executor { get; set; }
        public string RefDocument { get; set; }
        public string Data { get; set; }
        public string RefUsuario { get; set; }
        public virtual AplicacaoRecomendacoesTecnicasViewModel? AplicacaoRecomendacoesTecnicas { get; set; }
        public virtual ContratanteViewModel? Contratante { get; set; }
        public virtual IdentificacaoAreaTratadaViewModel? IdentificacaoAreaTratada { get; set; }
        public virtual CaracteristicasProdutoAplicadoViewModel? CaracteristicasProdutoAplicado { get; set; }
        public virtual ContratoPrestacaoServicoViewModel? ContratoPrestacaoServico { get; set; }
        public virtual DadosResponsavelViewModel? DadosResponsavel { get; set; }
    }
}
