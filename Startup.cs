using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestApiEmployees.Domain.Repositories;
using RestApiEmployees.Domain.Services;
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
            services.AddSingleton<IEmployeeService, EmployeeService>();

            //PG ADONET
            //services.AddSingleton<IEmployeeRepository, EmployeeRepositoryPG>();

            //PG EF
            services.AddDbContext<EmployeesDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("postgresql")));
            services.AddSingleton<IEmployeeRepository, EmployeeRepositoryPG>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}