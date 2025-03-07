using System.Text;
using BookManager.Infrastructure.Persistence;
using BookManager.Core.Repository;
using BookManager.Infrastructure.Persistence;
using BookManager.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace BookManager.Application
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddRepositories()
                .AddData(configuration);
            return services;
        }

        private static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("BookManagerCs");
            
            services.AddDbContext<BookManagerDbContext>(options => options.UseSqlServer(connectionString));

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBookRepository, BookRepository>();
            // services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }

        // private static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
        // {
        //     services.AddScoped<IAuthService, AuthService>();
        //
        //     services
        //         .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        //         .AddJwtBearer(o =>
        //         {
        //             o.TokenValidationParameters = new TokenValidationParameters
        //             {
        //                 ValidateIssuer = true,
        //                 ValidateAudience = true,
        //                 ValidateLifetime = true,
        //                 ValidateIssuerSigningKey = true,
        //                 ValidIssuer = configuration["Jwt:Issuer"],
        //                 ValidAudience = configuration["Jwt:Audience"],
        //                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
        //             };
        //         });
        //     
        //     return services;
        // }
    }
}

