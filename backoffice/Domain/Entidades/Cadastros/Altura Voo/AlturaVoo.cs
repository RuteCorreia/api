using System.ComponentModel.DataAnnotations;

namespace Domain.Entidades.Cadastros.Altura_Voo;

public class AlturaVoo
{
    [Key]
    public int Id { get; set; }
    public string Nome { get; set; }
}
