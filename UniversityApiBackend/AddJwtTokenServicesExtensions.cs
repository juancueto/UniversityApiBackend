using Microsoft.AspNetCore.Authentication.JwtBearer;
using UniversityApiBackend.Models.DataModels;
using Microsoft.IdentityModel.Tokens;

namespace UniversityApiBackend
{
    public static class AddJwtTokenServicesExtensions
    {
        public static void AddJwtTokenServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add 
            var bindJwtSettings = new JwtSettings();

            configuration.Bind("JsonWebTokenKeys", bindJwtSettings);

            // Add Singleton of JWT settings
            services.AddSingleton(bindJwtSettings);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters() { 
                    ValidateIssuerSigningKey = bindJwtSettings.ValidateIssuerSigningkey,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(bindJwtSettings.IssuerSigningkey)),
                    ValidateIssuer = bindJwtSettings.ValidateIssuer,
                    ValidIssuer = bindJwtSettings.ValidIssuer,
                    ValidateAudience = bindJwtSettings.ValidateAudience,
                    ValidAudience = bindJwtSettings.ValidAudience,
                    RequireExpirationTime = bindJwtSettings.RequireExpirationTime,
                    ValidateLifetime = bindJwtSettings.ValidateLifetime,
                    ClockSkew = TimeSpan.FromDays(1)
                };
            });
        }
    }
}
