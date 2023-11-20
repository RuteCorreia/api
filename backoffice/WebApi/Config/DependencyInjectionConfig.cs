using Application.Application.Servicos.Cadastros.Adjuvante;
using Application.Application.Servicos.Cadastros.Aeronave;
using Application.Application.Servicos.Cadastros.AlturaVoo;
using Application.Application.Servicos.Cadastros.AlvoBiologico;
using Application.Application.Servicos.Cadastros.Aplicacao;
using Application.Application.Servicos.Cadastros.AplicacaoAreaTratada;
using Application.Application.Servicos.Cadastros.AplicacaoCaracteristicas;
using Application.Application.Servicos.Cadastros.AplicacaoContrato;
using Application.Application.Servicos.Cadastros.AplicacaoCroqui;
using Application.Application.Servicos.Cadastros.AplicacaoCroquiImportacao;
using Application.Application.Servicos.Cadastros.AplicacaoLog;
using Application.Application.Servicos.Cadastros.AplicacaoRecomendacoesTecnicas;
using Application.Application.Servicos.Cadastros.AplicacaoRelatorio;
using Application.Application.Servicos.Cadastros.AplicacaoRelatorioItem;
using Application.Application.Servicos.Cadastros.Bula;
using Application.Application.Servicos.Cadastros.Cidades;
using Application.Application.Servicos.Cadastros.Cliente;
using Application.Application.Servicos.Cadastros.CombateIncendio;
using Application.Application.Servicos.Cadastros.CombateIncendioDecolagemPouso;
using Application.Application.Servicos.Cadastros.Combustivel;
using Application.Application.Servicos.Cadastros.ControleDeFrota;
using Application.Application.Servicos.Cadastros.Cultura;
using Application.Application.Servicos.Cadastros.Empresa;
using Application.Application.Servicos.Cadastros.Engenheiro;
using Application.Application.Servicos.Cadastros.Equipamento;
using Application.Application.Servicos.Cadastros.PlanoDeContrato;
using Application.DTOs.Cadastros.Adjuvante.Interface;
using Application.DTOs.Cadastros.Aeronave.Interface;
using Application.DTOs.Cadastros.AlturaVoo.Interface;
using Application.DTOs.Cadastros.AlvoBiologico.Interface;
using Application.DTOs.Cadastros.Aplicacao.Interface;
using Application.DTOs.Cadastros.AplicacaoAreaTratada.Interface;
using Application.DTOs.Cadastros.AplicacaoCaracteristicas.Interface;
using Application.DTOs.Cadastros.AplicacaoContrato.Interface;
using Application.DTOs.Cadastros.AplicacaoCroqui.Interface;
using Application.DTOs.Cadastros.AplicacaoCroquiImportacao.Interface;
using Application.DTOs.Cadastros.AplicacaoLog.Interface;
using Application.DTOs.Cadastros.AplicacaoRecomendacoesTecnicas.Interface;
using Application.DTOs.Cadastros.AplicacaoRelatorio.Interface;
using Application.DTOs.Cadastros.AplicacaoRelatorioItem.Interface;
using Application.DTOs.Cadastros.Bula.Interface;
using Application.DTOs.Cadastros.Cidades.Interface;
using Application.DTOs.Cadastros.Cliente.Interface;
using Application.DTOs.Cadastros.CombateIncendio.Interface;
using Application.DTOs.Cadastros.CombateIncendioDecolagemPouso.Interface;
using Application.DTOs.Cadastros.Combustivel.Interface;
using Application.DTOs.Cadastros.Controle_De_Frota.Interface;
using Application.DTOs.Cadastros.Cultura.Interface;
using Application.DTOs.Cadastros.Empresa.Interface;
using Application.DTOs.Cadastros.Engenheiro.Interface;
using Application.DTOs.Cadastros.Equipamento.Interface;
using Application.DTOs.Cadastros.PlanoDeContrato.Interface;
using Domain.Interfaces.Cadastros.Adjuvante;
using Domain.Interfaces.Cadastros.Aeronave;
using Domain.Interfaces.Cadastros.AlturaVoo;
using Domain.Interfaces.Cadastros.AlvoBiologico;
using Domain.Interfaces.Cadastros.Aplicacao;
using Domain.Interfaces.Cadastros.AplicacaoAreaTratada;
using Domain.Interfaces.Cadastros.AplicacaoCaracteristicas;
using Domain.Interfaces.Cadastros.AplicacaoContrato;
using Domain.Interfaces.Cadastros.AplicacaoCroqui;
using Domain.Interfaces.Cadastros.AplicacaoCroquiImportacao;
using Domain.Interfaces.Cadastros.AplicacaoLog;
using Domain.Interfaces.Cadastros.AplicacaoRecomendacoesTecnicas;
using Domain.Interfaces.Cadastros.AplicacaoRelatorio;
using Domain.Interfaces.Cadastros.AplicacaoRelatorioItem;
using Domain.Interfaces.Cadastros.Bula;
using Domain.Interfaces.Cadastros.Cidades;
using Domain.Interfaces.Cadastros.Cliente;
using Domain.Interfaces.Cadastros.CombateIncendio;
using Domain.Interfaces.Cadastros.CombateIncendioDecolagemPouso;
using Domain.Interfaces.Cadastros.Combustivel;
using Domain.Interfaces.Cadastros.ControleDeFrota;
using Domain.Interfaces.Cadastros.Cultura;
using Domain.Interfaces.Cadastros.Empresa;
using Domain.Interfaces.Cadastros.Engenheiro;
using Domain.Interfaces.Cadastros.Equipamento;
using Domain.Interfaces.Cadastros.PlanoDeContrato;
using Infra.Configuracao;
using Infra.Repositorio.Cadastros.Adjuvante;
using Infra.Repositorio.Cadastros.Aeronave;
using Infra.Repositorio.Cadastros.AlturaVoo;
using Infra.Repositorio.Cadastros.AlvoBiologico;
using Infra.Repositorio.Cadastros.Aplicacao;
using Infra.Repositorio.Cadastros.AplicacaoAreaTratada;
using Infra.Repositorio.Cadastros.AplicacaoCaracteristicas;
using Infra.Repositorio.Cadastros.AplicacaoContrato;
using Infra.Repositorio.Cadastros.AplicacaoCroqui;
using Infra.Repositorio.Cadastros.AplicacaoCroquiImportacao;
using Infra.Repositorio.Cadastros.AplicacaoLog;
using Infra.Repositorio.Cadastros.AplicacaoRecomendacoesTecnicas;
using Infra.Repositorio.Cadastros.AplicacaoRelatorio;
using Infra.Repositorio.Cadastros.AplicacaoRelatorioItem;
using Infra.Repositorio.Cadastros.Bula;
using Infra.Repositorio.Cadastros.Cidades;
using Infra.Repositorio.Cadastros.Cliente;
using Infra.Repositorio.Cadastros.CombateIncendio;
using Infra.Repositorio.Cadastros.CombateIncendioDecolagemPouso;
using Infra.Repositorio.Cadastros.Combustivel;
using Infra.Repositorio.Cadastros.Controle_De_Frota;
using Infra.Repositorio.Cadastros.Cultura;
using Infra.Repositorio.Cadastros.Empresa;
using Infra.Repositorio.Cadastros.Engenheiro;
using Infra.Repositorio.Cadastros.Equipamento;
using Infra.Repositorio.Cadastros.PlanoDeContrato;

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
        services.AddScoped<IAplicacaoService, AplicacaoService>();
        services.AddScoped<IAplicacaoAreaTratadaService, AplicacaoAreaTratadaService>();
        services.AddScoped<IAplicacaoCaracteristicasService, AplicacaoCaracteristicasService>();
        services.AddScoped<IAplicacaoContratoService, AplicacaoContratoService>();
        services.AddScoped<IAplicacaoCroquiService, AplicacaoCroquiService>();
        services.AddScoped<IAplicacaoCroquiImportacaoService, AplicacaoCroquiImportacaoService>();
        services.AddScoped<IAplicacaoLogService, AplicacaoLogService>();
        services.AddScoped<IAplicacaoRecomendacoesTecnicasService, AplicacaoRecomendacoesTecnicasService>();
        services.AddScoped<IAplicacaoRelatorioService, AplicacaoRelatorioService>();
        services.AddScoped<IAplicacaoRelatorioItemService, AplicacaoRelatorioItemService>();
        services.AddScoped<ICidadeService, CidadesService>();
        services.AddScoped<IClienteService, ClienteService>();
        services.AddScoped<ICombateIncendioService, CombateIncendioService>();
        services.AddScoped<ICombateIncendioDecolagemPousoService, CombateIncendioDecolagemPousoService>();
        services.AddScoped<ICombustivelService, CombustivelService>();
        services.AddScoped<IControleDeFrotaService, ControleDeFrotaService>();
        services.AddScoped<ICulturaService, CulturaService>();
        services.AddScoped<IEmpresaService, EmpresaService>();
        services.AddScoped<IBulaService, BulaService>();
        services.AddScoped<IPlanoDeContratoService, PlanoDeContratoService>();
        services.AddScoped<IEngenheiroService, EngenheiroService>();
        services.AddScoped<IEquipamentoService, EquipamentoService>();

        #endregion

        #region Repositories (AddScoped)

        services.AddScoped<IAdjuvanteRepository, AdjuvanteRepository>();
        services.AddScoped<IAeronaveRepository, AeronaveRepository>();
        services.AddScoped<IAlturaVooRepository, AlturaVooRepository>();
        services.AddScoped<IAlvoBiologicoRepository, AlvoBiologicoRepository>();
        services.AddScoped<IAplicacaoRepository, AplicacaoRepository>();
        services.AddScoped<IAplicacaoAreaTratadaRepository, AplicacaoAreaTratadaRepository>();
        services.AddScoped<IAplicacaoCaracteristicasRepository, AplicacaoCaracteristicasRepository>();
        services.AddScoped<IAplicacaoContratoRepository, AplicacaoContratoRepository>();
        services.AddScoped<IAplicacaoCroquiRepository, AplicacaoCroquiRepository>();
        services.AddScoped<IAplicacaoCroquiImportacaoRepository, AplicacaoCroquiImportacaoRepository>();
        services.AddScoped<IAplicacaoLogRepository, AplicacaoLogRepository>();
        services.AddScoped<IAplicacaoRecomendacoesTecnicasRepository, AplicacaoRecomendacoesTecnicasRepository>();
        services.AddScoped<IAplicacaoRelatorioRepository, AplicacaoRelatorioRepository>();
        services.AddScoped<IAplicacaoRelatorioItemRepository, AplicacaoRelatorioItemRepository>();
        services.AddScoped<ICidadeRepository, CidadesRepository>();
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<ICombateIncendioRepository, CombateIncendioRepository>();
        services.AddScoped<ICombateIncendioDecolagemPousoRepository, CombateIncendioDecolagemPousoRepository>();
        services.AddScoped<ICombustivelRepository, CombustivelRepository>();
        services.AddScoped<IControleDeFrotaRepository, ControleDeFrotaRepository>();
        services.AddScoped<ICulturaRepository, CulturaRepository>();
        services.AddScoped<IEmpresaRepository, EmpresaRepository>();
        services.AddScoped<IBulaRepository, BulaRepository>();
        services.AddScoped<IPlanoDeContratoRepository, PlanoDeContratoRepository>();
        services.AddScoped<IEngenheiroRepository, EngenheiroRepository>();
        services.AddScoped<IEquipamentoRepository, EquipamentoRepository>();

        #endregion

        services.AddScoped<ContextBase>();

        return services;
    }
}
