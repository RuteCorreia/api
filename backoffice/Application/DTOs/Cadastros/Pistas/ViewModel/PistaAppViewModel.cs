using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Cadastros.Pistas.ViewModel
{
    public class PistaAppViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double LAT { get; set; }
        public double LONG { get; set; }
        public int? IdEmpresa { get; set; }
    }
}
