using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using CompanyNetCore.Model;
using CompanyNetCore.Interfaces;
using CompanyNetCore.Helper;
using CompanyNetCore.Repositories;

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
            services.AddMvc();
            services.Configure<DbSettings>(Configuration.GetSection("DbSettings"));

            services.AddSingleton<IDbContext, DbContext>();
            services.AddScoped<IRepository<Employee>, EmployeeRepo>();
            services.AddScoped<IRepository<Address>, AddressRepo>();
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }
            app.UseStatusCodePages();
            app.UseMvc();
        }
    }
}
