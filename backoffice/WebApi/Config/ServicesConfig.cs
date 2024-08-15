using Infra.Configuracao;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using OfficeOpenXml;
using System.Text;
using System.Text.Json;

namespace WebApi.Config;

public static class ServicesConfig
{
    public static void AddServicesConfiguration(this IServiceCollection services)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        services.AddControllers()
        .AddJsonOptions(opts =>
        {
            opts.JsonSerializerOptions.IgnoreNullValues = true; // ou `DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull` para .NET 6+
        });
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
