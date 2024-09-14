using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AllStars.API.Extensions;

public static class AuthorizationExtensions
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services,  IConfigurationRoot configuration)
    {
        var key = Encoding.UTF8.GetBytes(configuration["AllStarsIdentityOptions:Secret"]!);
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["AllStarsIdentityOptions:Issuer"],
                ValidAudience = configuration["AllStarsIdentityOptions:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    Console.WriteLine("Authentication failed: " + context.Exception.Message);
                    return Task.CompletedTask;
                },
                OnTokenValidated = context =>
                {
                    Console.WriteLine("Token validated: " + context.SecurityToken);
                    return Task.CompletedTask;
                }
            };
        });

        return services;
    }
}
