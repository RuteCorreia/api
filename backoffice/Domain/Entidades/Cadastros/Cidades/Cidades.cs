using System.ComponentModel.DataAnnotations;

namespace Domain.Entidades.Cadastros.Cidades;

public class Cidades
{
    [Key]
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Sigla { get; set; }
}
