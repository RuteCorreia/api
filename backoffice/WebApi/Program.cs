using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using WebApi.Config;

var builder = WebApplication.CreateBuilder(args);

/// <summary>
/// Adiciona a configuração do Swagger para gerar a documentação da API.
/// </summary>
builder.AddSwaggerConfiguration();

/// Culture Info
var cultureInfo = new CultureInfo("pt-BR"); 
CultureInfo.DefaultThreadCurrentCulture = cultureInfo; 
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

// Configurações de ambientes
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddDbContext<ContextBase>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString);
});

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
/// <summary>
/// Adiciona configuração de serviços personalizados.
/// </summary>
builder.Services.AddServicesConfiguration();

var app = builder.Build();

/// <summary>
/// Configuração de CORS para permitir qualquer origem, método e cabeçalho.
/// Em produção, é recomendável restringir as origens permitidas por questões de segurança.
/// </summary>
/// <remarks>
/// Para um ambiente de produção, substitua AllowAnyOrigin com WithOrigins("https://seu-dominio-angular.com").
/// </remarks>
app.UseCors(options =>
    options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

/// <summary>
/// Configuração do Swagger.
/// Disponibiliza a interface de usuário do Swagger para explorar a API.
/// </summary>
app.UseSwaggerConfiguration();

/// <summary>
/// Redireciona todas as requisições HTTP para HTTPS.
/// </summary>
app.UseHttpsRedirection();

app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        // Log a exceção (substitua isso pelo seu logger de preferência)
        // Por exemplo: _logger.LogError(ex, "An unexpected error occurred.");

        // Define o código de status HTTP
        context.Response.StatusCode = 500;

        // Você pode retornar uma mensagem de erro personalizada ou detalhes específicos
        await context.Response.WriteAsJsonAsync(new { error = "An error occurred", message = ex.Message });

        // Re-dispara a exceção
        throw; // Isto re-dispara a exceção original
    }
});

/// <summary>
/// Configura autenticação.
/// Garante que os usuários sejam autenticados antes de acessar recursos protegidos.
/// </summary>
app.UseAuthentication();

/// <summary>
/// Configura autorização.
/// Garante que os usuários autenticados tenham as permissões necessárias para acessar recursos.
/// </summary>
app.UseAuthorization();

/// <summary>
/// Mapeia os controladores da API.
/// Define os endpoints da API com base nos controladores.
/// </summary>
app.MapControllers();

/// <summary>
/// Inicia a aplicação.
/// </summary>
app.Run();

