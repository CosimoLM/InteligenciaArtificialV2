using IA_V2.Core.Enum;
using IA_V2.Core.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IA_V2.Infrastructure.Repositories
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _sqlConn;
        private readonly IConfiguration _configuration;
        private readonly DatabaseProvider _provider;

        public DbConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
            _sqlConn = _configuration.GetConnectionString("ConnectionSqlServer") ?? string.Empty;

            var providerStr = _configuration.GetSection("DatabaseProvider").Value ?? "SqlServer";
            Provider = providerStr.Equals("MySql", StringComparison.OrdinalIgnoreCase)
                ? DatabaseProvider.MySql
                : DatabaseProvider.SqlServer;
        }

        public DatabaseProvider Provider { get; }

        public IDbConnection CreateConnection()
        {
            return Provider switch
            {
                DatabaseProvider .SqlServer=>   new SqlConnection(_sqlConn)
            };
        } 
    }
}
