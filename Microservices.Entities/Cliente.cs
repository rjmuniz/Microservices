using Microservices.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservices.Entities
{
    public class Cliente : IEntityId
    {
        public int Id { get; set; }
    }
}
