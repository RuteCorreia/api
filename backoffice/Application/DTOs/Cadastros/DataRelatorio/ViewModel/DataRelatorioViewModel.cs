using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Cadastros.DataRelatorio.ViewModel
{
    public class DataRelatorioViewModel
    {
        public int Id { get; set; }
        public string? Data { get; set; }
        public int? IdEmpresa { get; set; }
    }
}
