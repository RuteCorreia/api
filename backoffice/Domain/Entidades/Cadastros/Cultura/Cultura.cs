using System.ComponentModel.DataAnnotations;

namespace Domain.Entidades.Cadastros.Cultura;

public class Cultura
{
    [Key]
    public int IdCultura { get; set; }
    public string? Nome { get; set; }
    public string? AlvoBiologico { get; set; }
}
