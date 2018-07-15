using System;

namespace Microservices.Entities.Common
{
    public interface IEntityId : IEntity
    {
        int Id { get; }
    }
    public interface IEntityIdGuid : IEntity
    {
        Guid Id { get; }
    }
}
