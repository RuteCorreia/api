using Domain.Entidades.Cadastros.Cliente;
using Domain.Entidades.Cadastros.Empresa;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.User;

public class Usuario
{
    public Usuario(string email, string nome, string userId, int nrUsuario, string? telefone, int? idEmpresa, string cpf, decimal? comissao, bool gerarRelatorioManutencao, int? idCliente)
    {
        Id = Guid.NewGuid();
        Email = email;
        Nome = nome;
        UserId = userId;
        Removido = false;
        DataCriacao = DateTime.Now;
        NrUsuario = nrUsuario;
        PrimeiroAcesso = true;
        Telefone = telefone;
        IdEmpresa = idEmpresa;
        CPF = cpf;
        Comissao = comissao;
        IdCliente = idCliente;
        GerarRelatorioManutencao = gerarRelatorioManutencao;
        FlagTermoResp = false;
    }

    public Usuario(Guid id, string email, string nome, string userId, int nrUsuario)
    {
        Id = id;
        Email = email;
        Nome = nome;
        UserId = userId;
        Removido = false;
        DataCriacao = DateTime.Now;
        NrUsuario = nrUsuario;
    }

    [Key]
    public Guid Id { get; private set; }
    public string UserId { get; private set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public DateTime DataCriacao { get; private set; }
    public bool Removido { get; set; }
    public int NrUsuario { get; private set; }
    public string? Telefone { get; set; }
    public bool PrimeiroAcesso { get; set; }
    public byte[]? Assinatura { get; set; }
    public string CPF { get; set; }
    public decimal? Comissao { get; set; }
    public int? IdCliente { get; set; }
    public bool GerarRelatorioManutencao { get; set; }
    public bool? FlagTermoResp { get; set; }
    public DateTime DataSituacao { get; private set; } =
        TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));

    [ForeignKey("Empresa")]
    public int? IdEmpresa { get; set; }

    [JsonIgnore]
    public virtual Empresa? Empresa { get; set; }
}
