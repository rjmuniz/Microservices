using System;
using System.Collections.Generic;
using System.Text;

namespace Microservices.Entities.Common
{
    public static class IEntityExtensions
    {
        public static dynamic GetEntityId(this IEntity entity)
        {
            if (typeof(IEntityIdGuid).IsAssignableFrom(entity.GetType()))
                return ((IEntityIdGuid)entity).Id;
            else if (typeof(IEntityId).IsAssignableFrom(entity.GetType()))
                return ((IEntityId)entity).Id;
            else
                throw new ArgumentOutOfRangeException(nameof(entity));
        }
    }
}
