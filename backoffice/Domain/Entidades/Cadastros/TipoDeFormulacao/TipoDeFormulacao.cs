using System.ComponentModel.DataAnnotations;

namespace Domain.Entidades.Cadastros.TipoDeFormulacao
{
    public class TipoDeFormulacao
    {
        [Key]
        public int Id { get; set; }
        public string? NomeFormulacao { get; set; }
    }
}
