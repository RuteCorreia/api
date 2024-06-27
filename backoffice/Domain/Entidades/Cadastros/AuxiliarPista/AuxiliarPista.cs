using System.ComponentModel.DataAnnotations;

namespace Domain.Entidades.Cadastros.AuxiliarPista
{
    public class AuxiliarPista
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
    }
}
