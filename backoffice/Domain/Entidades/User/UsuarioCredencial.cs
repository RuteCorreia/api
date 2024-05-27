using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entidades.User;

public class UsuarioCredencial
{
    [Key]
    public Guid Id { get; set; }

    [ForeignKey("Usuario")]
    public Guid? IdUsuario { get; set; }

    [JsonIgnore]
    public virtual Usuario? Usuario { get; set; }

    public string? Credencial { get; set; }

    public ERole Funcao { get; set; }
}
