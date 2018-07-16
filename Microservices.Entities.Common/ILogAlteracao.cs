using System;
using System.Collections.Generic;
using System.Text;

namespace Microservices.Entities.Common
{
    public interface ILogAlteracao : ILogCadastro
    {
        Guid? UsuarioAlteracaoId { get; set; }

        DateTime? DataHoraAlteracao { get; set; }
    }
}
