using System.ComponentModel.DataAnnotations;

namespace Domain.Entidades.Cadastros.Tipo_Produto;

public class TipoProduto
{
    [Key]
    public int Id { get; set; }
    public string Nome { get; set; }
}
