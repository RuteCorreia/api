using System.ComponentModel.DataAnnotations;
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

        [ForeignKey("RelatorioManutencao")]
        public int? IdRelatorioManutencao { get; set; }
        public bool isSelected { get; set; }

        [ForeignKey("ManutencaoAeronaveItemsRevisao")]
        public int? IdManutencaoAeronaveItemsRevisao { get; set; }

        [JsonIgnore]
        public virtual ManutencaoAeronaveItemsRevisao.ManutencaoAeronaveItemsRevisao? ManutencaoAeronaveItemsRevisao { get; set; }

    }
}