using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DevIO.Business.Intefaces;
using DevIO.Business.Models;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevIO.Data.Repository
{
    // Classe repositório base, que trabalha com uma entidade genérica "TEntity", na qual essa entidade é um filha da classe mãe em Entity "Tentity : Entity".
    //      Então, quanlquer um que herde de Entity, pode ser passado aqui, e também pode ser estânciavel "new()"
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly MeuDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(MeuDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        // Método de buscar genérico, que obtem uma expression e retorna um objeto do tipo lista
        public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            // Retorna uma lista, carregada do banco de dados
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> ObterPorId(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<List<TEntity>> ObterTodos()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task Adicionar(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public virtual async Task Atualizar(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task Remover(Guid id)
        {
            // Podemos dar um "new TEntity" devido termos passado o argumento "new()", na herança da classe. Por este motivo,
            //      ao invés de irmos buscar no banco para depois remover, criamos uma instância de "new" que faz parte da classe
            //      mãe Tentity onde atribuimos o id (que faz parte da classe mãe Entity) ao id que recebemos na chamada do método
            //      "new TEntity { Id = id }". Ou seja, passamos apenas o objeto, ao invés de irmos buscá-lo no banco, já que o mesmo
            //      só necessita do id, e o TEntity possui o tipo da classe que estamos passando como genérica.
            DbSet.Remove(new TEntity { Id = id });
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}