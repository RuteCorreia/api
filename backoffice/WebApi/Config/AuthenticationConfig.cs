using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebApi.Config;

public static class AuthenticationConfig
{
    public static void AddAuthenticationConfig(this IServiceCollection services)
    {
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                RequireExpirationTime = true,
                ValidIssuer = services.BuildServiceProvider().GetRequiredService<IConfiguration>().GetSection("Jwt:Issuer").Value,
                ValidAudience = services.BuildServiceProvider().GetRequiredService<IConfiguration>().GetSection("Jwt:Audience").Value,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(services.BuildServiceProvider().GetRequiredService<IConfiguration>().GetSection("Jwt:Secret").Value))
            };
        });
    }
}