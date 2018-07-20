using Microservices.Data;
using Microservices.Entities.Common;
using Microservices.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Microservices.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly DataContext _context;
        public Repository(DataContext context)
        {
            _context = context;
        }

        private IQueryable<TEntity> Query
        {
            get { return Set.AsQueryable(); }
        }

        private DbSet<TEntity> Set
        {
            get { return _context.Set<TEntity>(); }
        }

        public async Task<int> CountAsync()
        {
            return await Set.CountAsync();
        }


        public virtual async Task<TEntity> FindByIdAsync(object entityId)
        {
            return await _context.FindAsync<TEntity>(entityId);
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Query.Where(predicate);
        }

        public virtual IQueryable<TEntity> FindAll(string include = "")
        {
            var query = Query;
            if (!string.IsNullOrEmpty(include))
                query = Query.Include(include);

            if (typeof(IEntityInativo).IsAssignableFrom(typeof(TEntity)))
            {
                query = query.Where(e => !(e as IEntityInativo).Inativo);
            }
            return query;
        }


        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null) { throw new ArgumentNullException(nameof(entity)); }

            if (typeof(ILogCadastro).IsAssignableFrom(typeof(TEntity)))
            {
                ((ILogCadastro)entity).DataHoraCadastro = DateTime.Now;
                //Todo: pegar id
                ((ILogCadastro)entity).UsuarioCadastroId = _context.AdminUserId;
            }
            if (!await CanSaveAsync(entity, true))
                return entity;

            var entry = _context.Entry(entity);
            entry.State = EntityState.Added;

            await BeforeSaveAsync(entity, true);


            _context.SaveChanges();



            await AfterSaveAsync(entity, true);


            return entity;
        }
        public virtual async Task<TEntity> UpdateAsync(object id, TEntity entity)
        {
            if (entity == null) { throw new ArgumentNullException(nameof(entity)); }
            var old = await FindByIdAsync(id);

            if (typeof(ILogAlteracao).IsAssignableFrom(typeof(TEntity)))
            {
                ((ILogAlteracao)entity).DataHoraAlteracao = DateTime.Now;
                //Todo: pegar id
                ((ILogAlteracao)entity).UsuarioAlteracaoId = _context.AdminUserId;
            }

            if (!await CanSaveAsync(entity, false))
                return old;

            await BeforeSaveAsync(entity, false);



            _context.Entry(old).CurrentValues.SetValues(entity);

            await _context.SaveChangesAsync();


            await AfterSaveAsync(entity, false);

            return entity;
        }




        public async Task RemoveAsync(TEntity entity)
        {
            Set.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task BeforeSaveAsync(TEntity entity, bool insert)
        {
            await Task.CompletedTask;
        }

        public virtual async Task AfterSaveAsync(TEntity entity, bool insert)
        {
            await Task.CompletedTask;
        }

        public virtual async Task<bool> CanSaveAsync(TEntity entity, bool insert)
        {
            return await Task.FromResult(true);
        }

        public void BeginTransation()
        {
            _context.Database.BeginTransaction();
        }

        public void RollbackTransation()
        {
            _context.Database.RollbackTransaction();
        }

        public void CommitTransaction()
        {
            _context.Database.CommitTransaction();
        }


    }
}
