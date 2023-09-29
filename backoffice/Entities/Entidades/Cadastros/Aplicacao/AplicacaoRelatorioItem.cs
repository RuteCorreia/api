using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entidades.Cadastros.Aplicacao
{
    public class AplicacaoRelatorioItem
    {
        public int Id { get; set; }

        [ForeignKey("AplicacaoRelatorio")]
        public int? IdAplicacaoRelatorio { get; set; }
        public string? HoraInicio { get; set; }
        public string? HorimetroInicial { get; set; }
        public string? HoraTermino { get; set; }
        public string? HorimetroTermino { get; set; }
        public string? TemperaturaInicial { get; set; }
        public string? TemperaturaFinal { get; set; }
        public string? UrInicial { get; set; }
        public string? UrFinal { get; set; }
        public string? VentoInicial { get; set; }
        public string? VentoFinal { get; set; }
        public string? ImagemDadosClimaticos { get; set; }
        public virtual AplicacaoRelatorio AplicacaoRelatorio { get; set; }
    }
}
