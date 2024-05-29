using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Cadastros.AplicacaoContrato.ViewModel;

public class AplicacaoContratoViewModel
{
    public int Id { get; set; }
    public int? IdAplicacao { get; set; }
    public int? IdUF { get; set; }
    public int? IdCidade { get; set; }
    public string? NomeCliente { get; set; }
    public string? CPFCliente { get; set; }
    public string? Assinatura { get; set; }
}
