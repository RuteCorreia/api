using System.ComponentModel.DataAnnotations;

namespace Domain.Entidades.Cadastros.Equipamento;

public class Equipamento
{
    [Key]
    public int Id { get; set; }
    public string Nome { get; set; }
}
