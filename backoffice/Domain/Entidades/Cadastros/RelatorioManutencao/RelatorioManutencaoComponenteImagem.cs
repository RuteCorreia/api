using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.RelatorioManutencao
{
    public class RelatorioManutencaoComponenteImagem
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Empresa")]
        public int? IdEmpresa { get; set; }


        [ForeignKey("RelatorioManutencaoComponente")]
        public int? IdRelatorioManutencaoComponente { get; set; } 

        public string? Legenda { get; set; }

        public string? Imagem { get; set; }

        [JsonIgnore]
        public virtual Empresa.Empresa? Empresa { get; set; }

        [JsonIgnore]
        public virtual RelatorioManutencaoComponente? RelatorioManutencaoComponente { get; set; }

    }
}