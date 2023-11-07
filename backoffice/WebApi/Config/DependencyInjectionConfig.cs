using Application.Application.Servicos.Cadastros.Combustivel;
using Application.DTOs.Cadastros.Combustivel.Interface;
using Domain.Interfaces.Cadastros.Combustivel;
using Infra.Configuracao;
using Infra.Repositorio.Cadastros.Combustivel;

namespace WebApi.Config;

public static class DependencyInjectionConfig
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));


        #region Services (AddScoped)

        services.AddScoped<ICombustivelService, CombustivelService>();

        #endregion

        #region Repositories (AddScoped)

        services.AddScoped<ICombustivelRepository, CombustivelRepository>();

        #endregion

        services.AddScoped<ContextBase>();

        return services;
    }
}
