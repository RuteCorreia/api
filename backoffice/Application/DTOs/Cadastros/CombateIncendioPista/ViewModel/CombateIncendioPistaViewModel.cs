using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Cadastros.CombateIncendioPista.ViewModel
{
    public class CombateIncendioPistaViewModel
    {
        public int Id { get; set; }
        public DateTime? HorarioChegadaPista { get; set; }
        public string? HorimetroChegadaPista { get; set; }
        public string? CodigoICAOPista { get; set; }
        public string? NomePista { get; set; }
        public string? LatPista { get; set; }
        public string? LongPista { get; set; }
        public string? IdEmpresa { get; set; }
        public int CombateIncendioId { get; set; }
    }
}
