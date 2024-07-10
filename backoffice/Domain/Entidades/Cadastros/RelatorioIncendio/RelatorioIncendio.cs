using Domain.Entidades.Cadastros.Pistas;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.RelatorioIncendio
{
    public class RelatorioIncendio
    {
        public int Id { get; set; }

        public bool IsMapa { get; set; }

        [ForeignKey("Pista")]
        public int PistaId { get; set; }

        [ForeignKey("LocalIncendio")]
        public int LocalIncendioId { get; set; }
        public List<int> DecolagemPousoFirefighting { get; set; }

        [ForeignKey("DadosResponsavel")]
        public int DadosResponsavelId { get; set; }

        [ForeignKey("CoordenadorBaseOperacional")]
        public int? CoordenadorBaseOperacionalId { get; set; }

        [ForeignKey("ComandanteOcorrencia")]
        public int? ComandanteOcorrenciaId { get; set; }

        [ForeignKey("ContratoPrestacaoServico")]
        public int ContratoPrestacaoServicoId { get; set; }

        [JsonIgnore]
        public virtual Pista? Pista { get; set; }

        [JsonIgnore]
        public virtual LocalIncendio.LocalIncendio? LocalIncendio { get; set; }

        [JsonIgnore]
        public virtual DadosResponsavel.DadosResponsavel? DadosResponsavel { get; set; }

        [JsonIgnore]
        public virtual DadosResponsavel.DadosResponsavel? CoordenadorBaseOperacional { get; set; }

        [JsonIgnore]
        public virtual DadosResponsavel.DadosResponsavel? ComandanteOcorrencia { get; set; }

        [JsonIgnore]
        public virtual ContratoPrestacaoServico.ContratoPrestacaoServico? ContratoPrestacaoServico { get; set; }
    }
}
