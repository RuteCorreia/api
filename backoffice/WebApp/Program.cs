using Data.Context;
using Data.Repositorio.Cadastros.Adjuvante;
using Data.Repositorio.Cadastros.Aeronave;
using Data.Repositorio.Cadastros.AlvoBiologico;
using Data.Repositorio.Cadastros.Bula;
using Data.Repositorio.Cadastros.Cliente;
using Data.Repositorio.Cadastros.Combustivel;
using Data.Repositorio.Cadastros.Cultura;
using Data.Repositorio.Cadastros.Empresa;
using Data.Repositorio.Cadastros.Engenheiro;
using Data.Repositorio.Cadastros.Equipamento;
using Data.Repositorio.Cadastros.Executor;
using Data.Repositorio.Cadastros.Frota;
using Data.Repositorio.Cadastros.Piloto;
using Data.Repositorio.Cadastros.Pista;
using Data.Repositorio.Cadastros.PlanoContrato;
using Data.Repositorio.Cadastros.Produto;
using Data.Repositorio.Cadastros.Veiculante;
using Data.Repositorio.Generico;
using Domain.Interfaces;
using Domain.Interfaces.Cadastros.Adjuvante;
using Domain.Interfaces.Cadastros.Aeronave;
using Domain.Interfaces.Cadastros.AlvoBiologico;
using Domain.Interfaces.Cadastros.Bula;
using Domain.Interfaces.Cadastros.Cliente;
using Domain.Interfaces.Cadastros.Combustivel;
using Domain.Interfaces.Cadastros.Cultura;
using Domain.Interfaces.Cadastros.Empresa;
using Domain.Interfaces.Cadastros.Engenheiro;
using Domain.Interfaces.Cadastros.Equipamento;
using Domain.Interfaces.Cadastros.Executor;
using Domain.Interfaces.Cadastros.Frota;
using Domain.Interfaces.Cadastros.Piloto;
using Domain.Interfaces.Cadastros.Pista;
using Domain.Interfaces.Cadastros.PlanoContrato;
using Domain.Interfaces.Cadastros.Produto;
using Domain.Interfaces.Cadastros.Veiculante;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Cadastros.Adjuvante;
using Domain.Servicos.Cadastros.Aeronave;
using Domain.Servicos.Cadastros.AlvoBiologico;
using Domain.Servicos.Cadastros.Bula;
using Domain.Servicos.Cadastros.Cliente;
using Domain.Servicos.Cadastros.Combustivel;
using Domain.Servicos.Cadastros.Cultura;
using Domain.Servicos.Cadastros.Empresa;
using Domain.Servicos.Cadastros.Engenheiro;
using Domain.Servicos.Cadastros.Equipamento;
using Domain.Servicos.Cadastros.Executor;
using Domain.Servicos.Cadastros.Frota;
using Domain.Servicos.Cadastros.Piloto;
using Domain.Servicos.Cadastros.Pista;
using Domain.Servicos.Cadastros.PlanoContrato;
using Domain.Servicos.Cadastros.Produto;
using Domain.Servicos.Cadastros.Veiculante;
using Domain.Servicos.Genericos;
using Entities.Entidades.Cadastros.Adjuvante;
using Entities.Entidades.Cadastros.Aeronaves;
using Entities.Entidades.Cadastros.Alvo_Biologico;
using Entities.Entidades.Cadastros.Cliente;
using Entities.Entidades.Cadastros.Combustivel;
using Entities.Entidades.Cadastros.Cultura;
using Entities.Entidades.Cadastros.Empresa;
using Entities.Entidades.Cadastros.Engenheiros;
using Entities.Entidades.Cadastros.Executores;
using Entities.Entidades.Cadastros.Frota;
using Entities.Entidades.Cadastros.Pilotos;
using Entities.Entidades.Cadastros.Pistas;
using Entities.Entidades.Cadastros.Produtos;
using Entities.Entidades.Cadastros.Veiculante;
using Entities.Entidades.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<DataContext>();
builder.Services.AddControllersWithViews();

#region Base
builder.Services.AddTransient<IBaseRepository<User>, BaseRepository<User>>();
builder.Services.AddTransient<IBaseService<User>, BaseService<User>>();
builder.Services.AddTransient<IBaseRepository<Bula>, BaseRepository<Bula>>();
builder.Services.AddTransient<IBaseService<Bula>, BaseService<Bula>>();
builder.Services.AddTransient<IBaseRepository<Cliente>, BaseRepository<Cliente>>();
builder.Services.AddTransient<IBaseService<Cliente>, BaseService<Cliente>>();
builder.Services.AddTransient<IBaseRepository<Empresa>, BaseRepository<Empresa>>();
builder.Services.AddTransient<IBaseService<Empresa>, BaseService<Empresa>>();
builder.Services.AddTransient<IBaseRepository<Engenheiro>, BaseRepository<Engenheiro>>();
builder.Services.AddTransient<IBaseService<Engenheiro>, BaseService<Engenheiro>>();
builder.Services.AddTransient<IBaseRepository<Executor>, BaseRepository<Executor>>();
builder.Services.AddTransient<IBaseService<Executor>, BaseService<Executor>>();
builder.Services.AddTransient<IBaseRepository<Piloto>, BaseRepository<Piloto>>();
builder.Services.AddTransient<IBaseService<Piloto>, BaseService<Piloto>>();
builder.Services.AddTransient<IBaseRepository<PlanoDeContrato>, BaseRepository<PlanoDeContrato>>();
builder.Services.AddTransient<IBaseService<PlanoDeContrato>, BaseService<PlanoDeContrato>>();
builder.Services.AddTransient<IBaseRepository<Cultura>, BaseRepository<Cultura>>();
builder.Services.AddTransient<IBaseService<Cultura>, BaseService<Cultura>>();
builder.Services.AddTransient<IBaseRepository<Veiculante>, BaseRepository<Veiculante>>();
builder.Services.AddTransient<IBaseService<Veiculante>, BaseService<Veiculante>>();
builder.Services.AddTransient<IBaseRepository<Combustivel>, BaseRepository<Combustivel>>();
builder.Services.AddTransient<IBaseService<Combustivel>, BaseService<Combustivel>>();
builder.Services.AddTransient<IBaseRepository<Pista>, BaseRepository<Pista>>();
builder.Services.AddTransient<IBaseService<Pista>, BaseService<Pista>>();
builder.Services.AddTransient<IBaseRepository<Produto>, BaseRepository<Produto>>();
builder.Services.AddTransient<IBaseService<Produto>, BaseService<Produto>>();
builder.Services.AddTransient<IBaseRepository<AlvoBiologico>, BaseRepository<AlvoBiologico>>();
builder.Services.AddTransient<IBaseService<AlvoBiologico>, BaseService<AlvoBiologico>>();
builder.Services.AddTransient<IBaseRepository<Frota>, BaseRepository<Frota>>();
builder.Services.AddTransient<IBaseService<Frota>, BaseService<Frota>>();
builder.Services.AddTransient<IBaseRepository<Aeronave>, BaseRepository<Aeronave>>();
builder.Services.AddTransient<IBaseService<Aeronave>, BaseService<Aeronave>>();
builder.Services.AddTransient<IBaseRepository<Adjuvante>, BaseRepository<Adjuvante>>();
builder.Services.AddTransient<IBaseService<Adjuvante>, BaseService<Adjuvante>>();
#endregion

#region Repository & Services
builder.Services.AddTransient<IBulaRepository, BulaRepository>();
builder.Services.AddTransient<IBulaService, BulaService>();
builder.Services.AddTransient<IClienteRepository, ClienteRepository>();
builder.Services.AddTransient<IClienteService, ClienteService>();
builder.Services.AddTransient<IEmpresaRepository, EmpresaRepository>();
builder.Services.AddTransient<IEmpresaService, EmpresaService>();
builder.Services.AddTransient<IEngenheiroRepository, EngenheiroRepository>();
builder.Services.AddTransient<IEngenheiroService, EngenheiroService>();
builder.Services.AddTransient<IExecutorRepository, ExecutorRepository>();
builder.Services.AddTransient<IExecutorService, ExecutorService>();
builder.Services.AddTransient<IPilotoRepository, PilotoRepository>();
builder.Services.AddTransient<IPilotoService, PilotoService>();
builder.Services.AddTransient<IPlanoContratoRepository, PlanoContratoRepository>();
builder.Services.AddTransient<IPlanoContratoService, PlanoContratoService>();
builder.Services.AddTransient<ICulturaRepository, CulturaRepository>();
builder.Services.AddTransient<ICulturaService, CulturaService>();
builder.Services.AddTransient<IVeiculanteRepository, VeiculanteRepository>();
builder.Services.AddTransient<IVeiculanteService, VeiculanteService>();
builder.Services.AddTransient<ICombustivelRepository, CombustivelRepository>();
builder.Services.AddTransient<ICombustivelService, CombustivelService>();
builder.Services.AddTransient<IPistaRepository, PistaRepository>();
builder.Services.AddTransient<IPistaService, PistaService>();
builder.Services.AddTransient<IProdutoRepository, ProdutoRepository>();
builder.Services.AddTransient<IProdutoService, ProdutoService>();
builder.Services.AddTransient<IAlvoBiologicoRepository, AlvoBiologicoRepository>();
builder.Services.AddTransient<IAlvoBiologicoService, AlvoBiologicoService>();
builder.Services.AddTransient<IFrotaRepository, FrotaRepository>();
builder.Services.AddTransient<IFrotaService, FrotaService>();
builder.Services.AddTransient<IAeronaveRepository, AeronaveRepository>();
builder.Services.AddTransient<IAeronaveService, AeronaveService>();
builder.Services.AddTransient<IEquipamentoRepository, EquipamentoRepository>();
builder.Services.AddTransient<IEquipamentoService, EquipamentoService>();
builder.Services.AddTransient<IAdjuvanteRepository, AdjuvanteRepository>();
builder.Services.AddTransient<IAdjuvanteService, AdjuvanteService>();

#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
