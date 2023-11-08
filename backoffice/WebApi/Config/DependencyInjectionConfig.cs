using Application.Application.Servicos.Cadastros.Cidades;
using Application.Application.Servicos.Cadastros.Combustivel;
using Application.DTOs.Cadastros.Cidades.Interface;
using Application.DTOs.Cadastros.Combustivel.Interface;
using Domain.Interfaces.Cadastros.Cidades;
using Domain.Interfaces.Cadastros.Combustivel;
using Infra.Configuracao;
using Infra.Repositorio.Cadastros.Cidades;
using Infra.Repositorio.Cadastros.Combustivel;

namespace WebApi.Config;

public static class DependencyInjectionConfig
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));


        #region Services (AddScoped)

        services.AddScoped<ICombustivelService, CombustivelService>();
        services.AddScoped<ICidadeService, CidadesService>();

        #endregion

        #region Repositories (AddScoped)

        services.AddScoped<ICombustivelRepository, CombustivelRepository>();
        services.AddScoped<ICidadeRepository, CidadesRepository>();

        #endregion

        services.AddScoped<ContextBase>();

        return services;
    }
}
