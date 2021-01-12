using System;
using System.Data;

namespace NetworkEquipments.Domain.Extensions
{
    public static class DbCommandExtension
    {
        public static IDbDataParameter AddParameter(this IDbCommand command, string name, object value, DbType dbType, ParameterDirection direction = ParameterDirection.Input)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Direction = direction;
            parameter.Value = value ?? DBNull.Value;
            parameter.DbType = dbType;
            return parameter;
        }
    }
}
