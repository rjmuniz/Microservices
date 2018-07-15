using System;
using System.Collections.Generic;
using System.Text;

namespace Microservices.Entities.Common
{
    public interface ILogCadastro
    {
        Guid UsuarioCadastroId { get; set; }
        IUsuario UsuarioCadastro { get; set; }

        DateTime DataHoraCadastro { get; set; }
    }
}
