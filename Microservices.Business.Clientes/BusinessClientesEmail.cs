using Microservices.Business.Common;
using Microservices.Entities;
using Microservices.Repository.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Business.Clientes
{
    public class BusinessClientesEmail : IBusinessBase<ClienteEmail>
    {
        private readonly IRepository<Cliente> _repositoryCliente;
        private readonly IRepository<Email> _repositoryEmail;
        public BusinessClientesEmail(IRepository<Cliente> repositoryCliente, IRepository<Email> repositoryEmail)
        {
            _repositoryCliente = repositoryCliente;
            _repositoryEmail = repositoryEmail;
        }


        public Task<int> CountAsync()
        {
            return _repositoryCliente.CountAsync();
        }

        public async Task<IQueryable<ClienteEmail>> FindAllActivesAsync()
        {
            return await FindAllAsync();
        }

        public async Task<IQueryable<ClienteEmail>> FindAllAsync()
        {
            var clientes = _repositoryCliente.FindAll("Emails");
            return await Task.FromResult(clientes.Select(c => ClienteEmail.ParseToClienteEmail(c)));

        }

        public async Task<ClienteEmail> FindByIdAsync(object entityId)
        {
            var c = await _repositoryCliente.FindByIdAsync(entityId, "Emails");
            return await Task.FromResult(ClienteEmail.ParseToClienteEmail(c));

        }

        public async Task<ClienteEmail> UpdateEntityAsync(object id, ClienteEmail entity)
        {
            var cliente = await _repositoryCliente.FindByIdAsync(id,"Emails");
            var emailPrincipal = ClienteEmail.ParseToEmail(entity);

            foreach (var email in cliente.Emails.Where(e => e.Principal && emailPrincipal.EnderecoEmail != e.EnderecoEmail))
            {
                email.Principal = false;
            }
            if (cliente.Emails.Any(e => !e.Principal && emailPrincipal.EnderecoEmail == e.EnderecoEmail))
                cliente.Emails.Single(e => emailPrincipal.EnderecoEmail== e.EnderecoEmail).Principal = true;


            await _repositoryCliente.Commit();


            if (!cliente.Emails.Any(e => e.Principal))
            {
                await _repositoryEmail.AddAsync(emailPrincipal);
                await _repositoryEmail.Commit();
            }


            return await FindByIdAsync(id);
        }













        public Task<ClienteEmail> AddAsync(ClienteEmail entity)
        {
            throw new NotImplementedException();
        }

        public Task AfterAddedAsync(ClienteEmail entity, bool insert)
        {
            throw new NotImplementedException();
        }

        public Task AfterUpdatedAsync(ClienteEmail entity, bool insert)
        {
            throw new NotImplementedException();
        }

        public Task BeforeAddAsync(ClienteEmail entity, bool insert)
        {
            throw new NotImplementedException();
        }

        public Task BeforeUpdateAsync(ClienteEmail entity, bool insert)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CanAddAsync(ClienteEmail entity, bool insert)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CanUpdateAsync(ClienteEmail entity, bool insert)
        {
            throw new NotImplementedException();
        }


        public Task RemoveAsync(object entityId)
        {
            throw new NotImplementedException();
        }


    }
}
