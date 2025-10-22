using Domain.Enums;

namespace Application.DTOs.Users.ViewModel;

public class UserDetailViewModel
{
    public string Id { get; set; }

    public string Nome { get; set; }

    public string Email { get; set; }
    public string? Telefone { get; set; }
    public string CPF { get; set; }
    public decimal? Comissao { get; set; }
    public bool GerarRelatorioManutencao { get; set; }
    public string FlagTermoResp { get; set; }

    public bool Removido { get; set; }

    public int? IdEmpresa { get; set; }

    //public ERole Funcao { get; set; }
    public IEnumerable<RoleObject> Funcoes { get; set; }

    public int? IdCliente { get; set; }
}