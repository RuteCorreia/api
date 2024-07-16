using System.ComponentModel.DataAnnotations;

namespace Domain.Entidades.Export_Excel
{
    public class PlanilhaExcelExportada
    {
        [Key]
        public int Id { get; set; }
        public string? Nome { get; set; }
        public Byte[]? Dados { get; set; }
    }
}
