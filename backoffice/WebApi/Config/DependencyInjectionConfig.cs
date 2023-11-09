using Application.Application.Servicos.Cadastros.Adjuvante;
using Application.Application.Servicos.Cadastros.Cidades;
using Application.Application.Servicos.Cadastros.Combustivel;
using Application.DTOs.Cadastros.Adjuvante.Interface;
using Application.DTOs.Cadastros.Cidades.Interface;
using Application.DTOs.Cadastros.Combustivel.Interface;
using Domain.Interfaces.Cadastros.Adjuvante;
using Domain.Interfaces.Cadastros.Cidades;
using Domain.Interfaces.Cadastros.Combustivel;
using Infra.Configuracao;
using Infra.Repositorio.Cadastros.Adjuvante;
using Infra.Repositorio.Cadastros.Cidades;
using Infra.Repositorio.Cadastros.Combustivel;

namespace WebApi.Config;

public static class DependencyInjectionConfig
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));


        #region Services (AddScoped)

        services.AddScoped<IAdjuvanteService, AdjuvanteService>();
        services.AddScoped<ICidadeService, CidadesService>();
        services.AddScoped<ICombustivelService, CombustivelService>();

        #endregion

        #region Repositories (AddScoped)

        services.AddScoped<IAdjuvanteRepository, AdjuvanteRepository>();
        services.AddScoped<ICidadeRepository, CidadesRepository>();
        services.AddScoped<ICombustivelRepository, CombustivelRepository>();

        #endregion

        services.AddScoped<ContextBase>();

        return services;
    }
}
