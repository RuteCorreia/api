using Domain.Entidades.Cadastros.Adjuvante;
using Domain.Entidades.Cadastros.Cidades;
using Domain.Entidades.Cadastros.Combustivel;
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
    public DbSet<Cidades> Cidades { get; set; }
    public DbSet<Combustivel> Combustivel { get; set; }

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
