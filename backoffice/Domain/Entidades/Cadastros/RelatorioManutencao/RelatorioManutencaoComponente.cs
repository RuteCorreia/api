using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.RelatorioManutencao
{
    public class RelatorioManutencaoComponente
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Empresa")]
        public int? IdEmpresa { get; set; }

        [ForeignKey("RelatorioManutencao")]
        public int? IdRelatorioManutencao { get; set; }

        [ForeignKey("Componentes")]
        public int? IdComponente { get; set; }

        public string? observacao { get; set; }

        [ForeignKey("ManutencaoAeronaveItemsRevisao")]
        public int? IdManutencaoAeronaveItemsRevisao { get; set; }

        [JsonIgnore]
        public virtual Empresa.Empresa? Empresa { get; set; }

        [JsonIgnore]
        public virtual RelatorioManutencao? RelatorioManutencao { get; set; }
        [JsonIgnore]
        public virtual Componentes.Componentes? Componentes { get; set; }
        [JsonIgnore]
        public virtual ManutencaoAeronaveItemsRevisao.ManutencaoAeronaveItemsRevisao? ManutencaoAeronaveItemsRevisao { get; set; }

    }
}