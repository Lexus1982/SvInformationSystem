using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using NetworkEquipments.Domain.ADO;
using NetworkEquipments.Domain.Entities;
using NetworkEquipments.Domain.Repository;
using NetworkEquipments.Domain.Extensions;

namespace NetworkEquipments.Domain.Repositories
{
    public class UserRoleRepository : AbstractRepository<UserRole>
    {
        public UserRoleRepository(AdoContext context) : base(context)
        {
        }

        protected override void Map(IDataRecord record, UserRole entity)
        {
            entity.UserId = (decimal)record["UserId"];
            entity.RoleId = (decimal)record["RoleId"];
        }

        public IEnumerable<UserRole> Get()
        {
            using (var command = Context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = @"MEDIATE.dbo.spNE_GetRoles";
                return ToList(command);
            }
        }
    }
}