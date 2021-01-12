using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using NetworkEquipments.Domain.Interfaces;

namespace NetworkEquipments.Domain.ADO
{ 
    public class DatabaseConnectionFactory : IDatabaseConnectionFactory
    {
        private readonly DbProviderFactory _provider;
        private readonly string _connectionString;
        private readonly string _providerName;
        
        public DatabaseConnectionFactory(string connectionString, string providerName) 
        {
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
            if (string.IsNullOrEmpty(providerName)) throw new ArgumentNullException(nameof(providerName));

            _provider = DbProviderFactories.GetFactory(providerName);
            _connectionString = connectionString;
            _providerName = providerName;
        }

        public IDbConnection Create()
        {
            var connection = _provider.CreateConnection();
            //var connection = new AseConnection(_connectionString);

            if (connection == null)
                throw new NullReferenceException($"Ошибка создания соединения с использованием провайдера '{_providerName}'.");

            connection.ConnectionString = _connectionString;
            connection.Open();
            return connection;
        }
    }
}
