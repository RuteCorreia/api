using Infra.Configuracao;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

namespace WebApi.Config;

public static class ServicesConfig
{
    public static void AddServicesConfiguration(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddDbContext<ContextBase>();
        services.AddDependencyInjection(services.BuildServiceProvider().GetRequiredService<IConfiguration>());
        services.AddAutoMapperConfig();
        services.AddIdentityConfig();
        services.AddAuthenticationConfig();
        services.AddHttpContextAccessor();
    }
}
