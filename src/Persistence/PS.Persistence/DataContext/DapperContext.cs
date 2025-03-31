using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Persistence.DataContext;


public class DapperContext
{
    private readonly IConfiguration _configuration;

    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IDbConnection CreateConnection()
    {
        string? connection = _configuration.GetConnectionString("PS");
        return new SqlConnection(connection);

    }

    public async Task<SqlConnection> CreateConnectionAsync()
    {
        try
        {
            var connectionString = _configuration.GetConnectionString("CHQPNTECO");
            var connection = new SqlConnection(connectionString);

            // Optionally, configure connection pooling here:
            // connection.ConnectionString += ";Pooling=true;MinPoolSize=10;MaxPoolSize=100";

            await connection.OpenAsync();
            return connection;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
