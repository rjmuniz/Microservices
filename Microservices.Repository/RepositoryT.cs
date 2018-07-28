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


        public virtual async Task<TEntity> FindByIdAsync(object entityId, string include = "")
        {
            var query = _context.Set<TEntity>().AsQueryable();
            if (!string.IsNullOrEmpty(include))
            {
                query = query.Include(include);
            }

            if (typeof(IEntityId).IsAssignableFrom(typeof(TEntity)))
                return await query.SingleOrDefaultAsync(e => ((IEntityId)e).Id == int.Parse(entityId.ToString()));
            else if (typeof(IEntityIdGuid).IsAssignableFrom(typeof(TEntity)))
                return await query.SingleOrDefaultAsync(e => ((IEntityIdGuid)e).Id == Guid.Parse(entityId.ToString()));
            else
                return await _context.Set<TEntity>().FindAsync(entityId);

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


        public virtual async Task AddAsync(TEntity entity)
        {
            if (entity == null) { throw new ArgumentNullException(nameof(entity)); }

            if (typeof(ILogCadastro).IsAssignableFrom(typeof(TEntity)))
            {
                ((ILogCadastro)entity).DataHoraCadastro = DateTime.Now;
                //Todo: pegar id
                ((ILogCadastro)entity).UsuarioCadastroId = _context.AdminUserId;
            }
            if (!await CanSaveAsync(entity, true))
                return;

            await _context.AddAsync(entity);


        }
        public virtual async Task UpdateAsync(object id, TEntity entity)
        {
            if (entity == null) { throw new ArgumentNullException(nameof(entity)); }

            var old = await FindByIdAsync(EntityHelper<TEntity>.GetTyped(id));

            if (typeof(ILogAlteracao).IsAssignableFrom(typeof(TEntity)))
            {
                ((ILogAlteracao)entity).DataHoraAlteracao = DateTime.Now;
                //Todo: pegar id
                ((ILogAlteracao)entity).UsuarioAlteracaoId = _context.AdminUserId;
            }

            if (!await CanSaveAsync(entity, false))
                return;

            _context.Entry(old).CurrentValues.SetValues(entity);

        }




        public async Task RemoveAsync(TEntity entity)
        {
            Set.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task BeforeSaveChangesAsync()
        {
            await Task.CompletedTask;
        }

        public virtual async Task AfterSaveChangesAsync()
        {
            await Task.CompletedTask;
        }

        public virtual async Task<bool> CanSaveAsync(TEntity entity, bool insert)
        {
            return await Task.FromResult(true);
        }



        public virtual async Task Commit()
        {
            await BeforeSaveChangesAsync();

            _context.SaveChanges();

            await AfterSaveChangesAsync();
        }


    }
}
