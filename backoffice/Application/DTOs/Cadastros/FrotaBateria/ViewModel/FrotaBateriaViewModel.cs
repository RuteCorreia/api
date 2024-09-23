using System.ComponentModel.DataAnnotations.Schema;

namespace Application.DTOs.Cadastros.FrotaBateria.ViewModel
{
    public class FrotaBateriaViewModel
    {
        public int? Id { get; set; }
        public int? IdBateria { get; set; }
        public int? IdFrota { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CicloInicial { get; set; }
        public int? CicloFinal { get; set; }
        public int? IdEmpresa { get; set; }
    }
}
