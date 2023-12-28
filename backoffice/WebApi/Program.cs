using WebApi.Config;

var builder = WebApplication.CreateBuilder(args);

builder.AddSwaggerConfiguration();
builder.Services.AddServicesConfiguration();

var app = builder.Build();

app.UseCors(options =>
    options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

//if (app.Environment.IsDevelopment())
//{
app.UseSwaggerConfiguration();
//}
//testes

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();