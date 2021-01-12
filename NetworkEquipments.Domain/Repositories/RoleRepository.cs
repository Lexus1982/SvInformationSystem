using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using NetworkEquipments.Domain.ADO;
using NetworkEquipments.Domain.Entities;
using NetworkEquipments.Domain.Extensions;
using NetworkEquipments.Domain.Repository;

namespace NetworkEquipments.Domain.Repositories
{
    public class RoleRepository : AbstractRepository<Role>
    {
        public RoleRepository(AdoContext context) : base(context) 
        {
        }

        protected override void Map(IDataRecord record, Role entity)
        {
            entity.Id = int.Parse(record["Id"].ToString());
            entity.Name = record.GetValueOrDefault<string>("Name");
        }

        public IEnumerable<Role> Get()
        {
            using (var command = Context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = @"MEDIATE.dbo.spNE_GetRoles";
                return ToList(command);
            }
        }

        public IEnumerable<Role> GetByUserId(decimal userId)
        {
            using (var command = Context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;

                command.CommandText = @"MEDIATE.dbo.spNE_GetRolesByUserId";
                command.Parameters.Add(command.AddParameter("USER_CODE", userId, DbType.Int32));
                return ToList(command);
            }
        }
    }
}
