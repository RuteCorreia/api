using Microsoft.OpenApi.Models;

namespace WebApi.Config;

public static class SwaggerConfig
{
    public static WebApplicationBuilder AddSwaggerConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(config =>
        {
            config.CustomSchemaIds(type => type.FullName);

            config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Exemplo: 'Bearer {token}'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            config.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });

            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (env == "QA")
            {
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Ambiente de QA", 
                    Version = "v1", 
                    Description = "Swagger API em ambiente QA.", 
                });
            }
            else if (env == "Production")
            {
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Ambiente de Produção",
                    Version = "v1",
                    Description = "Swagger API em ambiente de Produção.",
                });
            }
            else
            {
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Outro Ambiente", 
                    Version = "v1", 
                    Description = "Swagger API em outro ambiente.",
                });
            }
        });

        return builder;
    }

    public static WebApplication UseSwaggerConfiguration(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(config => { config.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"); });

        return app;
    }
}
