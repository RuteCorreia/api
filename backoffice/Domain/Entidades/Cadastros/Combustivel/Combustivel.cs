using System.ComponentModel.DataAnnotations;

namespace Domain.Entidades.Cadastros.Combustivel;

public class Combustivel
{
    [Key]
    public int Id { get; set; }
    public string Nome { get; set; }
}
