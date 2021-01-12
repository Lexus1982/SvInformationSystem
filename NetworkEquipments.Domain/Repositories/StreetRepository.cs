using System.Collections.Generic;
using System.Data;
using System.Linq;
using NetworkEquipments.Domain.ADO;
using NetworkEquipments.Domain.Entities;
using NetworkEquipments.Domain.Extensions;
using NetworkEquipments.Domain.Repository;

namespace NetworkEquipments.Domain.Repositories
{
    public class StreetRepository : AbstractRepository<Street>
    {
        public StreetRepository(AdoContext context) : base(context)
        {
        }

        protected override void Map(IDataRecord record, Street entity)
        {
            entity.Id = int.Parse(record["Id"].ToString());
            entity.Name = (string)record["Name"];
            entity.TownId = int.Parse(record["TownId"].ToString());
        }

        public IEnumerable<Street> Get()
        {
            using (var command = Context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = @"MEDIATE.dbo.spNE_GetStreets";
                return ToList(command);
            }
        }

        public IEnumerable<Street> GetByTownId(int id)
        {
            using (var command = Context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = @"MEDIATE.dbo.spNE_GetStreetsByTownId";
                command.Parameters.Add(command.AddParameter("TOWN_CODE", id, DbType.Int32));
                return ToList(command);
            }
        }

        public Street GetById(int id)
        {
            using (var command = Context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = @"MEDIATE.dbo.spNE_GetStreetById";
                command.Parameters.Add(command.AddParameter("STREET_CODE", id, DbType.Int32));
                return ToList(command).FirstOrDefault();
            }
        }
    }
}
