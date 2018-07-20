using System;
using System.Collections.Generic;
using System.Text;

namespace Microservices.Entities.Common
{
    public static class EntityHelper<TEntity>
    {
        public static dynamic GetTyped(object id)
        {
            return GetTyped(id.ToString());
        }
        public static dynamic GetTyped(string id)
        {
            if (typeof(IEntityIdGuid).IsAssignableFrom(typeof(TEntity)))
                return new Guid(id.ToString());
            else if (typeof(IEntityId).IsAssignableFrom(typeof(TEntity)))
                return int.Parse(id);
            else
                throw new ArgumentOutOfRangeException(nameof(id));
        }
        public static bool IsIdGuid()
        {
            return typeof(IEntityIdGuid).IsAssignableFrom(typeof(TEntity));
        }
        public static bool IsIdInt()
        {
            return typeof(IEntityId).IsAssignableFrom(typeof(TEntity));
        }
        
    }
}
