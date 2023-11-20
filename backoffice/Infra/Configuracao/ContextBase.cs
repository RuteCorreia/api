using Domain.Entidades.Cadastros.Adjuvante;
using Domain.Entidades.Cadastros.Aeronave;
using Domain.Entidades.Cadastros.Altura_Voo;
using Domain.Entidades.Cadastros.Alvo_Biologico;
using Domain.Entidades.Cadastros.Aplicacao;
using Domain.Entidades.Cadastros.Cidades;
using Domain.Entidades.Cadastros.Cliente;
using Domain.Entidades.Cadastros.CombateIncendio;
using Domain.Entidades.Cadastros.Combustivel;
using Domain.Entidades.Cadastros.Controle_De_Frota;
using Domain.Entidades.Cadastros.Cultura;
using Domain.Entidades.Cadastros.Empresa;
using Domain.Entidades.Cadastros.Engenheiro;
using Domain.Entidades.Cadastros.Equipamento;
using Domain.Entidades.Cadastros.Estados;
using Domain.Entidades.Cadastros.Executor;
using Domain.Entidades.Cadastros.Frota;
using Domain.Entidades.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infra.Configuracao;

public class ContextBase : IdentityDbContext<ApplicationUser>
{

    public ContextBase() {  }

    public ContextBase(DbContextOptions<ContextBase> options) : base(options)
    {
    }

    public DbSet<Adjuvante> Adjuvante { get; set; }
    public DbSet<Aeronave> Aeronave { get; set; }
    public DbSet<AlturaVoo> AlturaVoo { get; set; }
    public DbSet<AlvoBiologico> AlvoBiologico { get; set; }
    public DbSet<Aplicacao> Aplicacao { get; set; }
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
    public DbSet<PlanoDeContrato> PlanoDeContrato { get; set; }
    public DbSet<Engenheiro> Engenheiro { get; set; }
    public DbSet<Equipamento> Equipamento { get; set; }
    public DbSet<Estados> Estados { get; set; }
    public DbSet<Executor> Executor { get; set; }
    public DbSet<Frota> Frota { get; set; }

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
        builder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(t => t.Id);

        base.OnModelCreating(builder);
    }

    public string ObterStringConexao()
    {
        return "Data Source=198.38.83.200;Initial Catalog=Flytec;Integrated Security=False;User ID=keltec_user_dev_kel;Password=kel@123KL!#;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
    }
}
