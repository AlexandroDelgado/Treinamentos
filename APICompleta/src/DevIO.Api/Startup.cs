using AutoMapper;
using DevIO.Api.Configurations;
using DevIO.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace DevIO.Api
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
            // Inicia o "MeuDbContext"
            services.AddDbContext<MeuDbContext>(optionsAction: options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString(name: "DefaultConnection"));
                
            });

            // Adiciona a extensão do método AutoMapper
            services.AddAutoMapper(typeof(Startup));

            services.AddControllers();

            // Configurando os serviços automáticos
            services.Configure<ApiBehaviorOptions>(options =>
            {
                // Desativa a validação automática da API
                options.SuppressModelStateInvalidFilter = true;
            });

            // Resolve os problemas de dependência
            services.ResolveDependencies();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
