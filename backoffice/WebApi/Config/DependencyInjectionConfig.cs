using Application.Application.Servicos.Cadastros.Adjuvante;
using Application.Application.Servicos.Cadastros.Aeronave;
using Application.Application.Servicos.Cadastros.AlturaVoo;
using Application.Application.Servicos.Cadastros.AlvoBiologico;
using Application.Application.Servicos.Cadastros.Cidades;
using Application.Application.Servicos.Cadastros.Combustivel;
using Application.DTOs.Cadastros.Adjuvante.Interface;
using Application.DTOs.Cadastros.Aeronave.Interface;
using Application.DTOs.Cadastros.AlturaVoo.Interface;
using Application.DTOs.Cadastros.AlvoBiologico.Interface;
using Application.DTOs.Cadastros.Cidades.Interface;
using Application.DTOs.Cadastros.Combustivel.Interface;
using Domain.Interfaces.Cadastros.Adjuvante;
using Domain.Interfaces.Cadastros.Aeronave;
using Domain.Interfaces.Cadastros.AlturaVoo;
using Domain.Interfaces.Cadastros.AlvoBiologico;
using Domain.Interfaces.Cadastros.Cidades;
using Domain.Interfaces.Cadastros.Combustivel;
using Infra.Configuracao;
using Infra.Repositorio.Cadastros.Adjuvante;
using Infra.Repositorio.Cadastros.Aeronave;
using Infra.Repositorio.Cadastros.AlturaVoo;
using Infra.Repositorio.Cadastros.AlvoBiologico;
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
        services.AddScoped<IAeronaveService, AeronaveService>();
        services.AddScoped<IAlturaVooService, AlturaVooService>();
        services.AddScoped<IAlvoBiologicoService, AlvoBiologicoService>();
        services.AddScoped<ICidadeService, CidadesService>();
        services.AddScoped<ICombustivelService, CombustivelService>();

        #endregion

        #region Repositories (AddScoped)

        services.AddScoped<IAdjuvanteRepository, AdjuvanteRepository>();
        services.AddScoped<IAeronaveRepository, AeronaveRepository>();
        services.AddScoped<IAlturaVooRepository, AlturaVooRepository>();
        services.AddScoped<IAlvoBiologicoRepository, AlvoBiologicoRepository>();
        services.AddScoped<ICidadeRepository, CidadesRepository>();
        services.AddScoped<ICombustivelRepository, CombustivelRepository>();

        #endregion

        services.AddScoped<ContextBase>();

        return services;
    }
}
