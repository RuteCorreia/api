using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Cadastros.Veiculo.ViewModel;

public class VeiculoViewModel
{
    public int Id { get; set; }

    [Required]
    public string Marca { get; set; }

    [Required]
    public string Modelo { get; set; }

    [Required]
    [Length(7, 7, ErrorMessage = "A placa do veículo deve conter 7 caracteres")]
    public string Placa { get; set; }

    public int KM_Inicial { get; set; }
    public int KM_Atual { get; set; }
    public int KM_Inspecao { get; set; }
    public int KM_EntreRevisoes { get; set; }
    public int CapacidadeLitros { get; set; }
    public int QtdAtualLitros { get; set; }   
}
