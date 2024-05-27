using System.ComponentModel.DataAnnotations;

namespace Domain.Entidades.Cadastros.Estados;

public class Estados
{
    [Key]
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Sigla { get; set; }
}
