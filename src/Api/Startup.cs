
using System.Text;
using Api.Infrastructure.Registrations;
using Core.Context;
using Core.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using HappyCode.NetCoreBoilerplate.Api.Helpers;
using HappyCode.NetCoreBoilerplate.Api.Infrastructure.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.FeatureFilters;
using Microsoft.IdentityModel.Tokens;
using WebApi.Helpers;
using Microsoft.Extensions.Configuration;

namespace HappyCode.NetCoreBoilerplate.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services
                .AddHttpContextAccessor()
                .AddRouting(options => options.LowercaseUrls = true);

            services.AddCors(c =>
                {
                    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true) // allow any origin
                        );
                });

            services.AddMvcCore(options =>
                {
                    options.Filters.Add<HttpGlobalExceptionFilter>();
                    options.Filters.Add<ValidateModelStateFilter>();
                })
                .AddApiExplorer()
                .AddDataAnnotations()
                .SetCompatibilityVersion(CompatibilityVersion.Latest);

            // configure strongly typed settings object
            services.Configure<AppSettings>(_configuration.GetSection("AppSettings"));

            services.AddDbContext<EmployeesContext>(options => options.UseMySql(_configuration.GetConnectionString("MySqlDb")), contextLifetime: ServiceLifetime.Transient, optionsLifetime: ServiceLifetime.Singleton);
            services.AddDbContextPool<CategoriesContext>(options => options.UseSqlServer(_configuration.GetConnectionString("MsSqlDb")), poolSize: 10);

            services.AddSwagger(_configuration);

            
            services.AddCoreComponents();

            services.AddFeatureManagement()
                .AddFeatureFilter<TimeWindowFilter>();

            services.AddHealthChecks().AddSqlServer(_configuration.GetConnectionString("MsSqlDb"));

           
        }

        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true) // allow any origin
                );

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health", new HealthCheckOptions
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
                });
            });

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Simple Api V1");
                c.DocExpansion(DocExpansion.None);
            });
        }
    }
}
