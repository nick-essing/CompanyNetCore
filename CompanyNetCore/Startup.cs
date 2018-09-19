using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using CompanyNetCore.Model;
using CompanyNetCore.Interfaces;
using CompanyNetCore.Helper;
using CompanyNetCore.Repositories;
using Microsoft.AspNetCore.Http;
using TobitLogger.Core;
using TobitLogger.Middleware;
using TobitWebApiExtensions.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TobitLogger.Logstash;


namespace CompanyNetCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ILogContextProvider, RequestGuidContextProvider>();
            services.AddChaynsToken();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.Configure<DbSettings>(Configuration.GetSection("DbSettings"));
            services.AddSingleton<IDbContext, DbContext>();
            services.AddScoped<IRepository<Employee>, EmployeeRepo>();
            services.AddScoped<IRepository<Address>, AddressRepo>();
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, ILogContextProvider logContextProvider)
        {
            loggerFactory.AddLogstashLogger(Configuration.GetSection("Logger"), logContextProvider: logContextProvider);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
                app.UseExceptionHandler();
            }

            app.UseRequestLogging();
            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseStatusCodePages();
            app.UseMvc();
        }
    }
}
