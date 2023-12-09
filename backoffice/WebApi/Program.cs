using WebApi.Config;

var builder = WebApplication.CreateBuilder(args);

builder.AddSwaggerConfiguration();
builder.Services.AddServicesConfiguration();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerConfiguration();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();