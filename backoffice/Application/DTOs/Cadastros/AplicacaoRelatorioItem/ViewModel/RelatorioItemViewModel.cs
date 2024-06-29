using Application.DTOs.Cadastros.DataFormat.ViewModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Cadastros.AplicacaoRelatorioItem.ViewModel
{
    public class RelatorioItemViewModel
    {
        public int? Id { get; set; }
        public int? IdAplicacaoRelatorio { get; set; }
        public string? HoraInicio { get; set; }
        public string? HorimetroInicial { get; set; }
        public string? HoraFinal { get; set; }
        public string? HorimetroFinal { get; set; }
        public string? TemperaturaInicial { get; set; }
        public string? TemperaturaFinal { get; set; }
        public string? UmidadeRelativaArInicial { get; set; }
        public string? UmidadeRelativaArFinal { get; set; }
        public string? VentoInicial { get; set; }
        public string? VentoFinal { get; set; }
        public string? ImagemCondicaoClimatica { get; set; }
        public DateTime? DataAplicacao { get; set; }
    }
}
