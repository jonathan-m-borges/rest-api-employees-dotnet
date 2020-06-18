using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RestApiEmployees.Domain.Interfaces;
using RestApiEmployees.Domain.Services;
using RestApiEmployees.Persistence.Memory;
using RestApiEmployees.Persistence.PostgreEF;
using RestApiEmployees.Persistence.PostgreSQL;

namespace RestApiEmployees
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
            services.AddScoped<IEmployeesService, EmployeesService>();

            //In Memory
            //services.AddSingleton<IEmployeesRepository, EmployeesRepository>();

            //SQL in PostgreSQL
            //services.AddSingleton<IEmployeesRepository, EmployeesRepositorySQL>();

            //EntityFramework in PostgreSQL
            services.AddDbContext<EmployeesContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("postgresql")));
            services.AddScoped<IEmployeesRepository, EmployeesRepositoryEF>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //https removed
            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
