﻿using Microservices.Entities.Common;
using System;

namespace Microservices.Entities
{
    public  class Usuario: IUsuario
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }
    }
}
