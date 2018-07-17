using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservices.Repository.Interfaces
{
    public interface IRepositoryOptions
    {
        string ConnectionName { get; set; }

        string ConnectionString { get; }
    }
    public class RepositoryOptions : IRepositoryOptions
    {
        private readonly IConfiguration configuration;

        public RepositoryOptions(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string ConnectionName { get; set; } = "Database";

        public string ConnectionString
        {
            get
            {
                return configuration.GetConnectionString(ConnectionName);
            }
        }
    }
}
