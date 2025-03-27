using Domain.Entidades.Cadastros.Adjuvante;
using Domain.Entidades.Cadastros.Aeronave;
using Domain.Entidades.Cadastros.Altura_Voo;
using Domain.Entidades.Cadastros.Alvo_Biologico;
using Domain.Entidades.Cadastros.Aplicacao;
using Domain.Entidades.Cadastros.CaracteristicasProdutoAplicado;
using Domain.Entidades.Cadastros.Cidades;
using Domain.Entidades.Cadastros.Cliente;
using Domain.Entidades.Cadastros.CombateIncendio;
using Domain.Entidades.Cadastros.Combustivel;
using Domain.Entidades.Cadastros.Componentes;
using Domain.Entidades.Cadastros.Contratante;
using Domain.Entidades.Cadastros.ContratoPrestacaoServico;
using Domain.Entidades.Cadastros.Controle_De_Frota;
using Domain.Entidades.Cadastros.Cultura;
using Domain.Entidades.Cadastros.DadosResponsavel;
using Domain.Entidades.Cadastros.Empresa;
using Domain.Entidades.Cadastros.Equipamento;
using Domain.Entidades.Cadastros.Estados;
using Domain.Entidades.Cadastros.Frota;
using Domain.Entidades.Cadastros.IdentificacaoAreaTratada;
using Domain.Entidades.Cadastros.ManutencaoAeronave;
using Domain.Entidades.Cadastros.ManutencaoAeronaveItemsRevisao;
using Domain.Entidades.Cadastros.Menu;
using Domain.Entidades.Cadastros.MenuUsuario;
using Domain.Entidades.Cadastros.Pistas;
using Domain.Entidades.Cadastros.Precificacao;
using Domain.Entidades.Cadastros.Produto;
using Domain.Entidades.Cadastros.RelatorioAplicacao;
using Domain.Entidades.Cadastros.SubMenu;
using Domain.Entidades.Cadastros.Tipo_Produto;
using Domain.Entidades.Cadastros.Veiculante;
using Domain.Entidades.Importação_Planilha;
using Domain.Entidades.Log;
using Domain.Entidades.Cadastros.Veiculo;
using Domain.Entidades.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Domain.Entidades.Cadastros.RelatorioIncendio;
using Domain.Entidades.Cadastros.LocalIncendio;
using Domain.Entidades.Cadastros.AuxiliarPista;
using Domain.Entidades.Cadastros.DataRelatorio;
using Domain.Entidades.Cadastros.TipoDeServico;
using Domain.Entidades.Export_Excel;
using Domain.Entidades.Cadastros.RelatorioManutencao;
using Domain.Entidades.Cadastros.Municipio;
using Domain.Entidades.Cadastros.TipoDeFormulacao;
using Domain.Entidades.Cadastros.Bateria;
using Domain.Entidades.Cadastros.Gerador;
using Domain.Entidades.Cadastros.FrotaBateria;
using Domain.Entidades.Cadastros.FrotaGerador;
using Domain.Entidades.Cadastros.FrotaMotobomba;
using Microsoft.Extensions.Configuration;
using Domain.Entidades.Cadastros.IdentificadorAplicacao;
using Domain.Entidades.Cadastros.Motobomba;

namespace Infra.Configuracao;

public class ContextBase : IdentityDbContext
{
    private readonly IConfiguration _configuration;
    
    public ContextBase() {  }

    public ContextBase(DbContextOptions<ContextBase> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<Adjuvante> Adjuvante { get; set; }
    public DbSet<Aeronave> Aeronave { get; set; }
    public DbSet<AlturaVoo> AlturaVoo { get; set; }
    public DbSet<AlvoBiologico> AlvoBiologico { get; set; }
    public DbSet<Aplicacao> Aplicacao { get; set; }
    public DbSet<AuxiliarPista> AuxiliarPista { get; set; }
    public DbSet<AplicacaoAreaTratada> AplicacaoAreaTratada { get; set; }
    public DbSet<AplicacaoCaracteristicas> AplicacaoCaracteristicas { get; set; }
    public DbSet<AplicacaoContrato> AplicacaoContrato { get; set; }
    public DbSet<AplicacaoCroqui> AplicacaoCroqui { get; set; }
    public DbSet<AplicacaoCroquiImportacao> AplicacaoCroquiImportacao { get; set; }
    public DbSet<AplicacaoLog> AplicacaoLog { get; set; }
    public DbSet<AplicacaoRecomendacoesTecnicas> AplicacaoRecomendacoesTecnicas { get; set; }
    public DbSet<AplicacaoRelatorio> AplicacaoRelatorio { get; set; }
    public DbSet<AplicacaoRelatorioItem> AplicacaoRelatorioItem { get; set; }
    public DbSet<Cidades> Cidades { get; set; }
    public DbSet<Cliente> Cliente { get; set; }
    public DbSet<CombateIncendio> CombateIncendio { get; set; }
    public DbSet<CombateIncendioDecolagemPouso> CombateIncendioDecolagemPouso { get; set; }
    public DbSet<Combustivel> Combustivel { get; set; }
    public DbSet<ControleDeFrota> ControleDeFrota { get; set; }
    public DbSet<Cultura> Cultura { get; set; }
    public DbSet<Empresa> Empresa { get; set; }
    public DbSet<Bula> Bula { get; set; }
    public DbSet<BulaAplicacao> BulaAplicacao { get; set; }
    public DbSet<PlanoDeContrato> PlanoDeContrato { get; set; }
    public DbSet<Equipamento> Equipamento { get; set; }
    public DbSet<Estados> Estados { get; set; }
    public DbSet<Frota> Frota { get; set; }
    public DbSet<Pista> Pista { get; set; }
    public DbSet<Precificacao> Precificacao { get; set; }
    public DbSet<Produto> Produto { get; set; }
    public DbSet<TipoProduto> TipoProduto { get; set; }
    public DbSet<Veiculante> Veiculante { get; set; }
    public DbSet<Usuario> Usuario { get; set; }
    public DbSet<UsuarioCredencial> UsuarioCredencial { get; set; }
    public DbSet<Menu> Menu { get; set; }
    public DbSet<MenuUsuario> MenuUsuario { get; set; }
    public DbSet<SubMenu> SubMenu { get; set; }
    public DbSet<Componentes> Componente { get; set; }
    public DbSet<ManutencaoAeronave> ManutencaoAeronave { get; set; }
    public DbSet<ManutencaoAeronaveItemsRevisao> ManutencaoAeronaveItemsRevisao { get; set; }
    public DbSet<RelatorioAplicacao> RelatorioAplicacao { get; set; }
    public DbSet<RelatorioIncendio> RelatorioIncendio { get; set; }
    public DbSet<LocalIncendio> LocalIncendio { get; set; }
    public DbSet<Contratante> Contratante { get; set; }
    public DbSet<IdentificacaoAreaTratada> IdentificacaoAreaTratada { get; set; }
    public DbSet<CaracteristicasProdutoAplicado> CaracteristicasProdutoAplicado { get; set; }
    public DbSet<ContratoPrestacaoServico> ContratoPrestacaoServico { get; set; }
    public DbSet<DadosResponsavel> DadosResponsavel { get; set; }
    public DbSet<ImportacaoPlanilhas> ImportacaoPlanilha { get; set; }
    public DbSet<LogEntry> Logs { get; set; }
    public DbSet<Veiculo> Veiculo { get; set; }
    public DbSet<CombateIncendioPista> CombateIncendioPista { get; set; }
    public DbSet<DataRelatorio> DataRelatorio { get; set; }
    public DbSet<TipoDeServico> TipoDeServico { get; set; }
    public DbSet<PlanilhaExcelExportada> PlanilhaExcelExportadas { get; set; }
    public DbSet<TipoDeUnidade> TipoDeUnidade { get; set; }
    public DbSet<RelatorioManutencao> RelatorioManutencao { get; set; }
    public DbSet<RelatorioManutencaoComponente> RelatorioManutencaoComponente { get; set; }
    public DbSet<RelatorioManutencaoComponenteImagem> RelatorioManutencaoComponenteImagem { get; set; }
    public DbSet<RelatorioManutencaoRevisao> RelatorioManutencaoRevisao { get; set; }
    public DbSet<Municipio> Municipios { get; set; }
    public DbSet<TipoDeFormulacao> TipoDeFormulacao { get; set; }
    public DbSet<Bateria> Baterias { get; set; }
    public DbSet<Gerador> Geradores { get; set; }
    public DbSet<Motobomba> Motobombas { get; set; }
    public DbSet<FrotaBateria> FrotaBaterias { get; set; }
    public DbSet<FrotaGerador> FrotaGeradores { get; set; }
    public DbSet<FrotaMotobomba> FrotaMotobombas { get; set; }
    public DbSet<IdentificadorAplicacao> IdentificadorAplicacao { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(ObterStringConexao());
            base.OnConfiguring(optionsBuilder);
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Veiculo>()
            .HasIndex(x => x.Placa)
            .IsUnique();

        base.OnModelCreating(builder);
    }

    public string ObterStringConexao()
    {
        return _configuration.GetConnectionString("DefaultConnection");
    }
}
