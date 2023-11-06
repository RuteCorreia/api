using Microsoft.AspNet.Identity.EntityFramework;

namespace Domain.Entidades.User;

public class ApplicationUser : IdentityUser
{
    public string Nome { get; set; }
}
