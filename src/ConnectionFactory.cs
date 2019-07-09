using restlessmedia.Module.Configuration;
using SqlBuilder.DataServices;
using System;
using System.Data;
using System.Data.SqlClient;

namespace restlessmedia.Module
{
  internal class ConnectionFactory : IConnectionFactory
  {
    public ConnectionFactory(IDatabaseSettings databaseSettings)
    {
      _databaseSettings = databaseSettings ?? throw new ArgumentNullException(nameof(databaseSettings));
    }

    public IDbConnection CreateConnection(bool open = false)
    {
      IDbConnection connection = new SqlConnection(_databaseSettings.ConnectionString.ConnectionString);
      if (open)
      {
        connection.Open();
      }
      return connection;
    }

    public IDbTransaction CreateTransaction(IDbConnection connection)
    {
      return connection.BeginTransaction();
    }

    public IDbTransaction CreateTransaction(bool open = false)
    {
      return CreateTransaction(CreateConnection(open));
    }

    private readonly IDatabaseSettings _databaseSettings;
  }
}
