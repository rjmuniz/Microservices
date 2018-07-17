using System;
using System.Collections.Generic;
using System.Text;

namespace Microservices.Repository.Dapper
{
    public class DapperConfiguration<TEntity>
    {
        public string TableName { get; set; }
        public string Create { get; set; }
        public string FindById { get; set; }
        public string FindAll{ get; set; }
        public string Insert { get; set; }
        public Func<TEntity, Dictionary<string, object>> InsertValues { get; set; }
        public string Update { get; set; }
        public string Delete { get; set; }

    }
}
