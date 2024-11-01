using Infra.Configuracao;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OfficeOpenXml;
using System.Configuration;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebApi.Config;

public static class ServicesConfig
{
    public static void AddServicesConfiguration(this IServiceCollection services)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
        });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddDependencyInjection(services.BuildServiceProvider().GetRequiredService<IConfiguration>());
        services.AddAutoMapperConfig();
        services.AddIdentityConfig();
        services.AddAuthenticationConfig();
        services.AddHttpContextAccessor();
    }
}
