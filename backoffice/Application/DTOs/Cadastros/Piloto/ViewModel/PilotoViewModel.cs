using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Cadastros.Piloto.ViewModel;

public class PilotoViewModel
{
    public int IdPiloto { get; set; }
    public int? IdEmpresa { get; set; }
    public string NomePiloto { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public string CDAC { get; set; }
    public byte[] Assinatura { get; set; }
    public string PorcentagemComissao { get; set; }
}
