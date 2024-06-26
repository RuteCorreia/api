using Application.DTOs.Cadastros.ContratoPrestacaoServico.ViewModel;
using Application.DTOs.Cadastros.DadosResponsavel.ViewModel;
using Application.DTOs.Cadastros.LocalIncendio.ViewModel;
using Application.DTOs.Cadastros.Pistas.ViewModel;

namespace Application.DTOs.Cadastros.RelatorioIncendio.ViewModel
{
    public class RelatorioIncendioViewModel
    {
        public int Id { get; set; }
        public int PistaId { get; set; }
        public int LocalIncendioId { get; set; }
        public List<int> DecolagemPousoFirefighting { get; set; }
        public int DadosResponsavelId { get; set; }
        public int? CoordenadorBaseOperacionalId { get; set; }
        public int? ComandanteOcorrenciaId { get; set; }
        public int ContratoPrestacaoServicoId { get; set; }
        public virtual PistaViewModel? Pista { get; set; }
        public virtual LocalIncendioViewModel? LocalIncendio { get; set; }
        public virtual DadosResponsavelViewModel? DadosResponsavel { get; set; }
        public virtual DadosResponsavelViewModel? CoordenadorBaseOperacional { get; set; }
        public virtual DadosResponsavelViewModel? ComandanteOcorrencia { get; set; }
        public virtual ContratoPrestacaoServicoViewModel? ContratoPrestacaoServico { get; set; }
    }
}
