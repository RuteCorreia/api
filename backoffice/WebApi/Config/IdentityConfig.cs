using Infra.Configuracao;
using Microsoft.AspNetCore.Identity;

namespace WebApi.Config;

public static class IdentityConfig
{
    public static void AddIdentityConfig(this IServiceCollection services)
    {
        services.AddIdentity<IdentityUser, IdentityRole>(options =>
        {
            options.Password.RequiredLength = 6;
        })
        .AddEntityFrameworkStores<ContextBase>()
        .AddDefaultTokenProviders();
    }
}
