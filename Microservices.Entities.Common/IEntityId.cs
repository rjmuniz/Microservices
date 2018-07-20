using System;
using System.ComponentModel.DataAnnotations;

namespace Microservices.Entities.Common
{
    public interface IEntityId : IEntity
    {
        [Key]
        int Id { get; }
    }
    public interface IEntityIdGuid : IEntity
    {
        [Key]
        Guid Id { get; }
    }
}
