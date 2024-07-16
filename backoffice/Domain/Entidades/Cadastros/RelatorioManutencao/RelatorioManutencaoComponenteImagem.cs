using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.Cadastros.RelatorioManutencaoComponenteImagem
{
    public class RelatorioManutencaoComponenteImagem
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Empresa")]
        public int? IdEmpresa { get; set; }


        [ForeignKey("IdRelatorioManutencaoComponente")]
        public int? IdRelatorioManutencaoComponente

        public string? legenda;    

        public string? imagem;


    }
}