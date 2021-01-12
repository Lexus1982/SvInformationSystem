using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkEquipments.Domain.ADO;
using NetworkEquipments.Domain.Entities;
using NetworkEquipments.Domain.Extensions;
using NetworkEquipments.Domain.Repository;

namespace NetworkEquipments.Domain.Repositories
{
    public class TownRepository : AbstractRepository<Town>
    {
        public TownRepository(AdoContext context) : base(context)
        {
        }

        protected override void Map(IDataRecord record, Town entity)
        {
            entity.Id = int.Parse(record["Id"].ToString());
            entity.Name = (string)record["Name"];
        }

        public IEnumerable<Town> Get()
        {
            using (var command = Context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = @"MEDIATE.dbo.spNE_GetTowns";
                return ToList(command);
            }
        }

        public Town GetById(int id)
        {
            using (var command = Context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = @"MEDIATE.dbo.spNE_GetTownById";
                command.Parameters.Add(command.AddParameter("TOWN_CODE", id, DbType.Int32));
                return ToList(command).FirstOrDefault();
            }
        }
    }
}
