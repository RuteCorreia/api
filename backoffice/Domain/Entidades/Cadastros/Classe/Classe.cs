using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entidades.Cadastros.Classe
{
    [Table("Classe")]
    public class Classe
    {
        [Key]
        public int Id { get; set; }
        public string? Descricao { get; set; }

        [ForeignKey("TipoDeServico")]
        public int IdTipoDeServico { get; set; }
        public string ClassePublica { get; set; } = "N";
        public int? IdEmpresa { get; set; }

        [NotMapped]
        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}
