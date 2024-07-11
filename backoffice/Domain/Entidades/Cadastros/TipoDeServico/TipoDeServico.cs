using System.ComponentModel.DataAnnotations;

namespace Domain.Entidades.Cadastros.TipoDeServico
{
    public class TipoDeServico
    {
        [Key]
        public int Id { get; set; }
        public string? NomeServico { get; set; }
    }
}
