using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Core.Interfaces;
using Infrastructure.Services;
using Infrastructure.Repositories;
using TrazimMestra.Helpers;
using Core.interfaces;

namespace TrazimMestra.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddAutoMapper(typeof(MappingProfiles));

            services.AddDbContext<ApplicationContext>(options => {
                options.UseNpgsql(config.GetConnectionString("DefaultConnection"));
            });

            services.AddCors(o => o.AddPolicy("corspolicy", builder =>
            {
                builder.WithOrigins("http://localhost:4200")
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<INatjecajRepository, NatjecajRepository>();
            services.AddScoped<IMestarRepository, MestarRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<INatjecajRepository, NatjecajRepository>();
            services.AddScoped<IOfferRepository, OfferRepository>();

            services.AddAuthentication();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = false,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"])),
                     ValidIssuer = config["Token:Issuer"],
                     ValidateIssuer = true,
                     ValidateAudience = false
                 };
             });
            return services;
        }
    }
}
