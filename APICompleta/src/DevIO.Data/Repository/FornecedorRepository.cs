using System;
using System.Threading.Tasks;
using DevIO.Business.Intefaces;
using DevIO.Business.Models;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevIO.Data.Repository
{
    // Classe que está implementando o repositório base: Repository<Fornecedor> e a interface do repositório IFornecedorRepository
    public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
    {
        // Cria um construtor para o contexto, herdando o context de base
        public FornecedorRepository(MeuDbContext context) : base(context)
        {
        }

        // Os métodos abaixo, foram atribuidos no FornecedorRepository por não serem métodos genéricos, mas sim métodos especializados.
        public async Task<Fornecedor> ObterFornecedorEndereco(Guid id) // Método para obter o endereço do fornecedor
        {
            return await Db.Fornecedores.AsNoTracking() // Sempre utilizem o AsNoTracking ao invés do Tracking para obterem uma melhor performance.
                .Include(c => c.Endereco) // Inclui o endereço do fornecedor ao retorno do método
                .FirstOrDefaultAsync(c => c.Id == id); // Através do id recebido no método
        }

        public async Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id) // Método para obter o Fornecedor, seus produtos e endereço do mesmo.
        {
            return await Db.Fornecedores.AsNoTracking()
                .Include(c => c.Produtos) // Adiciona todos os produtos do fornecedor ao retorno do método
                .Include(c => c.Endereco) // Adiciona o endereço do fornecedor ao retorno do método
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}