using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Tekton.Ecommerce.Infrastructure.Data
{
    public class DapperContext
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;
        public DapperContext(IConfiguration _configuration)
        {
            this.configuration = _configuration;
            this.connectionString = _configuration.GetConnectionString("VirtualStoreConnection").ToString();
        }

        public IDbConnection createConnection() => new SqlConnection(connectionString);
    }
}
