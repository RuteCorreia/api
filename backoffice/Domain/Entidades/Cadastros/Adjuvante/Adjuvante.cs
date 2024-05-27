using System.ComponentModel.DataAnnotations;

namespace Domain.Entidades.Cadastros.Adjuvante;

public class Adjuvante
{
    [Key]
    public int Id { get; set; }
    public string Nome { get; set; }
}
