using System.ComponentModel.DataAnnotations;

namespace Domain.Entidades.Cadastros.Veiculante;

public class Veiculante
{
    [Key]
    public int IdVeiculante { get; set; }
    public string Nome { get; set; }
}
