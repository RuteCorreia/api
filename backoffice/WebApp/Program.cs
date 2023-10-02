using Data.Context;
using Data.Repositorio.Empresa;
using Data.Repositorio.Generico;
using Domain.Interfaces;
using Domain.Interfaces.Empresa;
using Domain.Servicos.Empresa;
using Domain.Servicos.Genericos;
using Entities.Entidades.Cadastros.Empresa;
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
builder.Services.AddTransient<IBaseRepository<User>, BaseRepository<User>>();
builder.Services.AddTransient<IBaseService<User>, BaseService<User>>();
builder.Services.AddTransient<IEmpresaRepository, EmpresaRepository>();
builder.Services.AddTransient<IBaseRepository<Empresa>, BaseRepository<Empresa>>();
builder.Services.AddTransient<IBaseService<Empresa>, BaseService<Empresa>>();
builder.Services.AddTransient<IEmpresaService, EmpresaService>();

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
