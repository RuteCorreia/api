using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.RelatorioManutencaoComponente
{
    public class RelatorioManutencaoComponente
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Empresa")]
        public int? IdEmpresa { get; set; }

        [ForeignKey("IdRelatorioManutencao")]
        public int? IdRelatorioManutencao { get; set; }


        [ForeignKey("IdComponente")]
        public int? IdComponente

        public string? observacao;    


        [ForeignKey("IdManutencaoAeronaveItemsRevisao")]
        public int? IdManutencaoAeronaveItemsRevisao



    }
}