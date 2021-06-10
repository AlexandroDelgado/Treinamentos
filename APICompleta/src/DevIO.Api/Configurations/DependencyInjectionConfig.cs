using DevIO.Business.Intefaces;
using DevIO.Business.Notificacoes;
using DevIO.Business.Services;
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

			// Adiciona o repositorio ao escopo
			services.AddScoped<IProdutoRepository, ProdutoRepository>();
			services.AddScoped<IFornecedorRepository, FornecedorRepository>();
			services.AddScoped<IEnderecoRepository, EnderecoRepository>();

			// Adiciona o notificador ao escopo
			services.AddScoped<INotificador, Notificador>();

			// Adiciona o serviço ao escopo
			services.AddScoped<IFornecedorService, FornecedorService>();
            services.AddScoped<IProdutoService, ProdutoService>();

			// retorna todos os serviços
			return services;
        }

    }
}
