using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entidades.Cadastros.ReceituarioAgronomico;

public class ReceituarioAgronomico
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("RelatorioAplicacao")]
    public int? RelatorioAplicacaoId { get; set; }
    public string? NomeArquivo { get; set; }
    public string? Numero { get; set; }
    public string? DataEmissao { get; set; }
}
