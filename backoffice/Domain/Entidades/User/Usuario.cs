using System.ComponentModel.DataAnnotations;

namespace Domain.Entidades.User;

public class Usuario
{
    public Usuario(string email, string nome, string userId)
    {
        Id = Guid.NewGuid();
        Email = email;
        Nome = nome;
        UserId = userId;
        Removido = false;
        DataCriacao = DateTime.Now;
    }

    [Key]
    public Guid Id { get; private set; }
    public string UserId { get; private set; }
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public DateTime DataCriacao { get; private set; }
    public bool Removido { get; set; }
}
