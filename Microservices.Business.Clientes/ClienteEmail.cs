using Microservices.Business.Common;
using Microservices.Entities;
using Microservices.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microservices.Business.Clientes
{
    public class ClienteEmail : IEntityBusiness
    {
        public int ClienteId { get; set; }
        public string Apelido { get; set; }

        public string EmailPrincipal { get; set; }


        public static ClienteEmail ParseToClienteEmail(Cliente _clente)
        {
            return new ClienteEmail
            {
                ClienteId = _clente.Id,
                Apelido = _clente.Apelido,
                EmailPrincipal = _clente?.Emails?.FirstOrDefault(c => c.Principal)?.EnderecoEmail
            };
        }
        public static Email ParseToEmail(ClienteEmail _clenteEmail)
        {
            return new Email
            {
                EnderecoEmail = _clenteEmail.EmailPrincipal,
                PessoaId = _clenteEmail.ClienteId,
                Principal = true
            };
        }
        public static Cliente ParseToCliente(ClienteEmail _clenteEmail)
        {
            return new Cliente
            {
                Id = _clenteEmail.ClienteId,
                Apelido = _clenteEmail.Apelido
            };
        }
    }
}
