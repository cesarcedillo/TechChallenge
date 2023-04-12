using Backend.TechChallenge.Application;
using Backend.TechChallenge.Infrastructure.Persistence;
using Backend.TechChallenge.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System;
using System.IO;

namespace Backend.TechChallenge.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddApplicationServices();
            services.AddInfrastructureServices(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            CreateDB(app);
        }

        private void CreateDB(IApplicationBuilder app)
        {
            var dbName = @$"{Directory.GetCurrentDirectory()}\{Configuration.GetConnectionString("ConnectionStringSqlLite")}";

            if (!File.Exists(dbName))
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var service = scope.ServiceProvider;
                    var loggerFactory = service.GetRequiredService<ILoggerFactory>();
                    try
                    {
                        var context = service.GetRequiredService<UserDbContext>();
                        context.Database.EnsureCreated();
                        UserDbContextSeed.SeedAsync(context);
                    }
                    catch (Exception ex)
                    {
                        var logger = loggerFactory.CreateLogger("Program");
                        logger.LogError(ex, "Error in Migration");
                    }
                }
            }
        }
    }
}
