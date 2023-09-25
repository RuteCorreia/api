using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HudAsp.Data;
using Domain.Interfaces;
using Service.Services;
using Domain.Entidades.User;
using Data.Repository;
using Data.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<DataContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IBaseRepository<User>, BaseRepository<User>>();
builder.Services.AddTransient<IBaseService<User>, BaseService<User>>();

// Add Sidebar menu json file
builder.Configuration.AddJsonFile("sidebar.json", optional: true, reloadOnChange: true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseEndpoints(endpoints =>
{
	endpoints.MapAreaControllerRoute(  
			name: "Identity",  
			areaName: "Identity",  
			pattern: "Identity/{controller=Home}/{action=Index}");  
	endpoints.MapControllers();
});
app.MapRazorPages();

app.Run();

