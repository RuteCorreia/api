using Application.DTOs.Cadastros.ProdutoAplicado.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ExportExcel.ViewModel
{
    public class ExportRelatorioViewModel
    {
        public string? UF { get; set; }
        public string? Municipio { get; set; }
        public string? TipoAeronave { get; set; }
        public string? PrefixoAeronave { get; set; }
        public string? HorasAplicacao { get; set; }
        public string? Cultura { get; set; }
        public string? TipoDeServico { get; set; }
        public string? ClasseAgrotoxico { get; set; }
        public decimal? Area { get; set; }
        public string? Agrotoxico { get; set; }
        public string? Adjuvante { get; set; }
        public string? Semeadura { get; set; }
        public string? HorasCombateIncendio { get; set; }
        public int? Volume { get; set; }
        public string? UnidadeVolume { get; set; }
        public decimal? Dosagem { get; set; }
        public string? UnidadeDosagem { get; set; }
        public List<ProdutoAplicadoCaracteristicasViewModel>? Produtos { get; set; }
    }
}
