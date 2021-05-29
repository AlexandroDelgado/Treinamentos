using DevIO.Business.Intefaces;
using DevIO.Data.Context;
using DevIO.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace DevIO.Api.Configurations
{
    public static class DependencyInjectionConfig
    {
        // Método de extensão para o IServiceCollection
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            // Adiciona o contexto ao escopo
            services.AddScoped<MeuDbContext>();

            // Adiciona o repositório do fornecedor escopo
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();

            // retorna todos os serviços
            return services;
        }

    }
}
