using Data.Context;
using Data.Repositorio.Cadastros.Bula;
using Data.Repositorio.Cadastros.Cliente;
using Data.Repositorio.Cadastros.Empresa;
using Data.Repositorio.Cadastros.Engenheiro;
using Data.Repositorio.Cadastros.Executor;
using Data.Repositorio.Cadastros.Piloto;
using Data.Repositorio.Cadastros.PlanoContrato;
using Data.Repositorio.Generico;
using Domain.Interfaces;
using Domain.Interfaces.Cadastros.Bula;
using Domain.Interfaces.Cadastros.Cliente;
using Domain.Interfaces.Cadastros.Empresa;
using Domain.Interfaces.Cadastros.Engenheiro;
using Domain.Interfaces.Cadastros.Executor;
using Domain.Interfaces.Cadastros.Piloto;
using Domain.Interfaces.Cadastros.PlanoContrato;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Cadastros.Bula;
using Domain.Servicos.Cadastros.Cliente;
using Domain.Servicos.Cadastros.Empresa;
using Domain.Servicos.Cadastros.Engenheiro;
using Domain.Servicos.Cadastros.Executor;
using Domain.Servicos.Cadastros.Piloto;
using Domain.Servicos.Cadastros.PlanoContrato;
using Domain.Servicos.Genericos;
using Entities.Entidades.Cadastros.Cliente;
using Entities.Entidades.Cadastros.Empresa;
using Entities.Entidades.Cadastros.Engenheiros;
using Entities.Entidades.Cadastros.Executores;
using Entities.Entidades.Cadastros.Pilotos;
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
