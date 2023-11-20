using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Cadastros.Frota.ViewModel;

public class FrotaViewModel
{
    public int Id { get; set; }
    public int? IdEmpresa { get; set; }
    public string NomeVeiculo { get; set; }
    public string Placa { get; set; }
    public string Frota { get; set; }
    public string Hodometro { get; set; }
}
