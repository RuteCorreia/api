using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Cadastros.Aplicacao.ViewModel;

public class AplicacaoViewModel
{
    public int Id { get; set; }
    public int? IdEmpresa { get; set; }
    public string StatusEnvio { get; set; }
    public int? IdPiloto { get; set; }
    public int? IdExecutor { get; set; }
    public int? IdCliente { get; set; }
    public int? IdCultura { get; set; }
}
