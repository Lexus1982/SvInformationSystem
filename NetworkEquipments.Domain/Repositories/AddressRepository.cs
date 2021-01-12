using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkEquipments.Domain.ADO;
using NetworkEquipments.Domain.Entities;
using NetworkEquipments.Domain.Extensions;
using NetworkEquipments.Domain.Interfaces;
using NetworkEquipments.Domain.Repository;

namespace NetworkEquipments.Domain.Repositories
{
    public class AddressRepository : AbstractRepository<Address>
    {
        public AddressRepository(AdoContext context) : base(context)
        {
        }

        protected override void Map(IDataRecord record, Address entity)
        {
            entity.Id = int.Parse(record["Id"].ToString());
            entity.House = (string)record["House"];
            entity.Corp = record.GetValueOrDefault<string>("Corp");
            entity.StreetId = int.Parse(record["StreetId"].ToString());
        }

        public IEnumerable<Address> Get()
        {
            using (var command = Context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = @"MEDIATE.dbo.spNE_GetAddresses";
                return ToList(command);
            }
        }

        public IEnumerable<Address> GetByStreetId(int id)
        {
            using (var command = Context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = @"MEDIATE.dbo.spNE_GetAddressesByStreetId";
                command.Parameters.Add(command.AddParameter("STREET_CODE", id, DbType.Int32));
                return ToList(command);
            }
        }

        public Address GetById(int id)
        {
            using (var command = Context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = @"MEDIATE.dbo.spNE_GetAddressById";
                command.Parameters.Add(command.AddParameter("ADDRESS_CODE", id, DbType.Int32));
                return ToList(command).FirstOrDefault();
            }
        }
    }
}
