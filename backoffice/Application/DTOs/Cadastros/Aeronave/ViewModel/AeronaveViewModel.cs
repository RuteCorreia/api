using Domain.Entidades.Cadastros.Empresa;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Cadastros.Aeronave.ViewModel;

public class AeronaveViewModel
{
    public int Id { get; set; }
    public int? IdEmpresa { get; set; }
    public string? Prefixo { get; set; }
    public string? Combustivel { get; set; }
    public int? CapacidadeDeCarga { get; set; }
    public string? Horimetro { get; set; }
}
