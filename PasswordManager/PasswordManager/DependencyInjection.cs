using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PasswordManager.Repositories.Contracts;
using PasswordManager.Repositories.Services;
using System.Text;

namespace PasswordManager
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPasswordManagerServices(this IServiceCollection services)
        {
            services
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IInfoRepository, InfoRepository>();

            return services;
        }

        public static IServiceCollection ConfigureJwtAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var keyBytes = Encoding.UTF8.GetBytes("VerySecretKeyThatNeedsToBeLongToWork");
            var signingKey = new SymmetricSecurityKey(keyBytes);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = configuration["Jwt:Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey
            };
            services
                .AddAuthentication(o => {
                    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(o => {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = true;
                    o.TokenValidationParameters = tokenValidationParameters;
                    o.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = _ => Task.CompletedTask,
                        OnTokenValidated = _ => Task.CompletedTask
                    };
                });
            return services;
        }
    }
}
