using System.ComponentModel.DataAnnotations;

namespace Domain.Entidades.Cadastros.Pistas;

public class Pista
{
    [Key]
    public int Id { get; set; }
    public string Nome { get; set; }
    public string LAT { get; set; }
    public string LONG { get; set;}
}
