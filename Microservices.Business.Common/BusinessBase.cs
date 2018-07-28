using Microservices.Entities.Common;
using Microservices.Repository.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Business.Common
{
    public  class BusinessBase<TEntity> : IBusinessBase<TEntity> where TEntity : class, IEntity
    {
        protected readonly IRepository<TEntity> _repository;
      
        public BusinessBase(IRepository<TEntity> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }




        public async Task<int> CountAsync()
            => await _repository.CountAsync();

        public virtual async Task<IQueryable<TEntity>> FindAllAsync()
            => await Task.FromResult(_repository.FindAll());

        public async Task<IQueryable<TEntity>> FindAllActivesAsync()
        {
            var query = _repository.FindAll();

            if (typeof(IEntityInativo).IsAssignableFrom(typeof(TEntity)))
            {
                query = query.Where(e => (e as IEntityInativo).Inativo == false);
            }
            return await Task.FromResult(query);
        }

        public async Task<TEntity> FindByIdAsync(object entityId)
            => await _repository.FindByIdAsync(entityId);


        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (!await CanAddAsync(entity, true)) return entity;

            await BeforeAddAsync(entity, true);

            await _repository.AddAsync(entity);
            await _repository.Commit();

            await AfterAddedAsync(entity, true);

            return entity;
        }
        //todo: separar os eventos onsave e onupdate
        public async Task<TEntity> UpdateEntityAsync(object entityId, TEntity entity)
        {
            if (!await CanUpdateAsync(entity, false)) return entity;

            await BeforeUpdateAsync(entity, false);

            await _repository.UpdateAsync(entityId, entity);

            await AfterUpdatedAsync(entity, false);

            return entity;
        }

        public async Task RemoveAsync(object entityId)
        {
            var entity = await FindByIdAsync(entityId);
            await _repository.RemoveAsync(entity);
        }


        public virtual async Task BeforeAddAsync(TEntity entity, bool insert) => await Task.CompletedTask;

        public virtual async Task AfterAddedAsync(TEntity entity, bool insert) => await Task.CompletedTask;

        public virtual async Task<bool> CanAddAsync(TEntity entity, bool insert) => await Task.FromResult(true);

        public virtual async Task BeforeUpdateAsync(TEntity entity, bool insert) => await Task.CompletedTask;

        public virtual async Task AfterUpdatedAsync(TEntity entity, bool insert) => await Task.CompletedTask;


        public virtual async Task<bool> CanUpdateAsync(TEntity entity, bool insert) => await Task.FromResult(true);


    }
}
