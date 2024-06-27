using Domain.Entidades.Cadastros.Empresa;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.User;

public class Usuario
{
    public Usuario(string email, string nome, string userId, int nrUsuario, string? telefone, int? idEmpresa, string cpf, bool gerarRelatorioManutencao)
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
        GerarRelatorioManutencao = gerarRelatorioManutencao;
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
    public bool GerarRelatorioManutencao { get; set; }

    [ForeignKey("Empresa")]
    public int? IdEmpresa { get; set; }

    [JsonIgnore]
    public virtual Empresa? Empresa { get; set; }
}
