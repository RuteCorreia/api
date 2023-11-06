
using Microsoft.AspNetCore.Identity;

namespace Domain.Entidades.User;

public class ApplicationUser : IdentityUser
{
    public string Nome { get; set; }
}
