using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using OnlineBookShop.Application.Common.Interfaces;
using OnlineBookShop.Infrastructure.Persistance.Options;
using System.Data;

namespace OnlineBookShop.Infrastructure.Persistance.Database
{
    public class SqlConnectionFactory: ISqlConnectionFactory, IDisposable
    {
        private string _connectionString;
        private IDbConnection _connection;

        public SqlConnectionFactory(IOptions<OnlineBookShopDbContextOptions> options)
        {
            _connectionString = options.Value.ConnectionString;
        }

        public void UseConnectionString(string connectionString)
        { 
            _connectionString = connectionString;
        }

        public IDbConnection GetOpenConnection()
        {
            if (_connection == null || _connection.State != ConnectionState.Open)
            {
                _connection = new SqlConnection(_connectionString);
                _connection.Open();
            }

            return _connection;
        }

        public void Dispose()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Dispose();
            }
        }
    }
}
