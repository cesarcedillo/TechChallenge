using Backend.TechChallenge.Application.Contracts.Persistence;
using Backend.TechChallenge.Infrastructure.Persistence;
using Backend.TechChallenge.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Reflection;

namespace Backend.TechChallenge.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UserDbContext>(options =>
                //options.UseSqlite(configuration.GetConnectionString("ConnectionString"))
                options.UseSqlite(@$"FileName={Directory.GetCurrentDirectory()}\{configuration.GetConnectionString("ConnectionStringSqlLite")}", option =>
                {
                    option.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                })
            );

            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
