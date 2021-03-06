﻿using Microservices.Business.Common;
using Microservices.Entities.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Api.Common
{
    public class ControllerBusiness<TEntity> : ControllerBase where TEntity : class, IEntity
    {
        private readonly IBusinessBase<TEntity> _business;

        public ControllerBusiness(IBusinessBase<TEntity> business)
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
            var result = await _business.FindByIdAsync(EntityHelper<TEntity>.GetTyped(id));

            if (result == null)
                return NotFound($"Not Found {typeof(TEntity).Name}({id})");

            return new ObjectResult(result);
        }


        // POST api/values
        [HttpPost]
        public virtual async Task<ActionResult<TEntity>> PostAsync([FromBody] TEntity entity)
        {
            try
            {
                return await _business.AddAsync(entity);
            }
            catch (Exception e)
            {
                return BadRequest(e.ExceptionAsString());
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public virtual async Task<ActionResult<TEntity>> PutAsync(string id, [FromBody] TEntity entity)
        {
            if (typeof(IEntityBusiness).IsAssignableFrom(typeof(TEntity)))
                return await _business.UpdateEntityAsync(id, entity);
            else
            {
                var entityId = entity?.GetEntityId()?.ToString();
                if (!entityId.Equals(id))
                    return NotFound($"Id:{id} != entityId:{entityId}");
                return await _business.UpdateEntityAsync(EntityHelper<TEntity>.GetTyped(id), entity);
            }            
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public virtual async Task Delete(string id)
            => await _business.RemoveAsync(EntityHelper<TEntity>.GetTyped(id));
    }
}
