using Infra.Configuracao;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OfficeOpenXml;
using System.Configuration;
using System.Text;
using System.Text.Json;

namespace WebApi.Config;

public static class ServicesConfig
{
    public static void AddServicesConfiguration(this IServiceCollection services)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        services.AddControllers()
        .AddJsonOptions(opts =>
        {
            opts.JsonSerializerOptions.IgnoreNullValues = true; // ou `DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull` para .NET 6+
        });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        var serviceProvider = services.BuildServiceProvider();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        services.AddDbContext<ContextBase>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            options.UseSqlServer(connectionString);
        });
        services.AddDependencyInjection(services.BuildServiceProvider().GetRequiredService<IConfiguration>());
        services.AddAutoMapperConfig();
        services.AddIdentityConfig();
        services.AddAuthenticationConfig();
        services.AddHttpContextAccessor();
    }
}
