using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Cadastros.AplicacaoCaracteristicas.ViewModel;

public class AplicacaoCaracteristicasViewModel
{
    public int Id { get; set; }
    public int? IdAplicacao { get; set; }
    public int? IdProduto { get; set; }
    public int? IdAdjuvante { get; set; }
    public string TipoDeServico { get; set; }
}
