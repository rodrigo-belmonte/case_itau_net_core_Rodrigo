using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseItau.Infra.Data
{
    public class CaseItauContext
    {
        private readonly string _connectionString;

        public CaseItauContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("caseItauConnectionString");

        }

        public IDbConnection CreateConnection()
            => new SqliteConnection(_connectionString);
    }
}
