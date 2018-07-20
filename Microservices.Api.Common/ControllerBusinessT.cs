using Microservices.Business.Common;
using Microservices.Entities.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Api.Common
{
    public  class ControllerBusiness<TEntity> : ControllerBase where TEntity : class, IEntity
    {
        private readonly BusinessBase<TEntity> _business;

        public ControllerBusiness(BusinessBase<TEntity> business)
        {
            _business = business;
        }
        // GET api/values
        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TEntity>>> GetAsync()
            => (await _business.FindAllActivesAsync()).ToArray();


        // GET api/values/5
        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TEntity>> GetAsync(string id)
        {
            object _id = null;
            if (typeof(IEntityIdGuid).IsAssignableFrom(typeof(TEntity)))
            {
                _id = new Guid(id);
            }
            else
                _id = int.Parse(id);

            return await _business.FindByIdAsync(_id);
        }

        // POST api/values
        [HttpPost]
        public virtual async Task<TEntity> PostAsync([FromBody] TEntity entity)
            => await _business.AddAsync(entity);

        // PUT api/values/5
        [HttpPut("{id}")]
        public virtual async Task<TEntity> PutAsync(object id, [FromBody] TEntity entity)
            => await _business.UpdateEntityAsync(id, entity);

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public virtual async Task Delete(object id) =>         
            await _business.RemoveAsync(id);
    }
}
