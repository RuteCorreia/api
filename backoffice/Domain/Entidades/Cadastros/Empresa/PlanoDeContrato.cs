using System.ComponentModel.DataAnnotations;

namespace Domain.Entidades.Cadastros.Empresa;

public class PlanoDeContrato
{
    [Key]
    public int IdPlano { get; set; }
    public string NomeDoPlano { get; set; }
}
