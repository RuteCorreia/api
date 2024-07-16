using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.RelatorioManutencao
{
    public class RelatorioManutencaoRevisao
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Empresa")]
        public int? IdEmpresa { get; set; }

        [ForeignKey("IdRelatorioManutencao")]
        public int? IdRelatorioManutencao { get; set; }

        public bool isSelected;
        
        [ForeignKey("IdManutencaoAeronaveItemsRevisao")]
        public int? IdManutencaoAeronaveItemsRevisao

 
    }
}