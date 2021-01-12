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
    public class NetworkEquipmentRepository : AbstractRepository<NetworkEquipment>
    {
        public NetworkEquipmentRepository(AdoContext context) : base(context)
        {
        }

        protected override void Map(IDataRecord record, NetworkEquipment entity)
        {
            entity.Id = int.Parse(record["Id"].ToString());
            entity.NetworkId = int.Parse(record["NetworkId"].ToString());
            entity.AddressId = int.Parse(record["AddressId"].ToString());
            entity.Entrance = record.GetValueOrDefault<int?>("Entrance");
            entity.EquipmentTypeId = int.Parse(record["EquipmentTypeId"].ToString());
            entity.Ip = record.GetValueOrDefault<string>("Ip");
            entity.Commentary = record.GetValueOrDefault<string>("Commentary");
            entity.UserId = int.Parse(record["UserId"].ToString());
            entity.ChangeDate = (DateTime)record["ChangeDate"];
        }

        public IEnumerable<NetworkEquipment> Get()
        {
            using (var command = Context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = @"MEDIATE.dbo.spNE_GetNetworkEquipments";
                return ToList(command);
            }
        }

        public NetworkEquipment GetById(int id)
        {
            using (var command = Context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = @"MEDIATE.dbo.spNE_GetNetworkEquipmentById";
                command.Parameters.Add(command.AddParameter("EQUIPMENT_CODE", id, DbType.Int32));
                return ToList(command).FirstOrDefault();
            }
        }

        public IEnumerable<NetworkEquipment> GetByNetworkId(int networkId)
        {
            using (var command = Context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = @"MEDIATE.dbo.spNE_GetEquipmentsByNetworkId";
                command.Parameters.Add(command.AddParameter("NETWORK_CODE", networkId, DbType.Int32));
                return ToList(command);
            }
        }

        public IEnumerable<NetworkEquipment> Search(string searchValue)
        {
            using (var command = Context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = @"MEDIATE.dbo.spNE_SearchNetworkEquipments";
                command.Parameters.Add(command.AddParameter("SEARCH_VALUE", searchValue, DbType.String));
                return ToList(command);
            }
        }

        public IEnumerable<NetworkEquipment> Search(int networkId, string searchValue)
        {
            using (var command = Context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = @"MEDIATE.dbo.spNE_SearchEquipmentsInNetwork";
                command.Parameters.Add(command.AddParameter("NETWORK_CODE", networkId, DbType.Int32));
                command.Parameters.Add(command.AddParameter("SEARCH_VALUE", searchValue, DbType.String));
                return ToList(command);
            }
        }

        public NetworkEquipment GetByIpAddress(string ipAddress)
        {
            using (var command = Context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;

                command.CommandText = @"MEDIATE.dbo.spNE_GetNetworkEquipmentByIpAddress";
                command.Parameters.Add(command.AddParameter("IP_ADDRESS", ipAddress, DbType.String));
                return ToList(command).FirstOrDefault();
            }
        }

        public int Create(NetworkEquipment entity)
        {
            using (var command = Context.CreateCommand())
            {
                var returnValue = command.AddParameter("return", entity.Id, DbType.Int32, ParameterDirection.ReturnValue);

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = @"MEDIATE.dbo.spNE_AddNetworkEquipment";
                command.Parameters.Add(command.AddParameter("NETWORK_CODE", entity.NetworkId, DbType.Int32));
                command.Parameters.Add(command.AddParameter("ADDRESS_CODE", entity.AddressId, DbType.Int32));
                command.Parameters.Add(command.AddParameter("ENTRANCE", entity.Entrance, DbType.Int32));
                command.Parameters.Add(command.AddParameter("EQUIPMENT_TYPE_CODE", entity.EquipmentTypeId, DbType.Int32));
                command.Parameters.Add(command.AddParameter("IP", entity.Ip, DbType.String));
                command.Parameters.Add(command.AddParameter("COMMENTARY", entity.Commentary, DbType.String));
                command.Parameters.Add(command.AddParameter("USER_CODE", entity.UserId, DbType.Int32));
                command.Parameters.Add(returnValue);
                command.ExecuteNonQuery();

                return (int)returnValue.Value;
            }
        }

        public int Delete(int id)
        {
            using (var command = Context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = @"MEDIATE.dbo.spNE_DelNetworkEquipment";
                command.Parameters.Add(command.AddParameter("EQUIPMENT_CODE", id, DbType.Int32));
                return command.ExecuteNonQuery();
            }
        }

        public int Update(NetworkEquipment entity)
        {
            using (var command = Context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = @"MEDIATE.dbo.spNE_UpdNetworkEquipment";
                command.Parameters.Add(command.AddParameter("EQUIPMENT_CODE", entity.Id, DbType.Int32));
                command.Parameters.Add(command.AddParameter("NETWORK_CODE", entity.NetworkId, DbType.Int32));
                command.Parameters.Add(command.AddParameter("ADDRESS_CODE", entity.AddressId, DbType.Int32));
                command.Parameters.Add(command.AddParameter("ENTRANCE", entity.Entrance, DbType.Int32));
                command.Parameters.Add(command.AddParameter("EQUIPMENT_TYPE_CODE", entity.EquipmentTypeId, DbType.Int32));
                command.Parameters.Add(command.AddParameter("IP", entity.Ip, DbType.String));
                command.Parameters.Add(command.AddParameter("COMMENTARY", entity.Commentary, DbType.String));
                command.Parameters.Add(command.AddParameter("USER_CODE", entity.UserId, DbType.Int32));
                return command.ExecuteNonQuery();
            }
        }
    }
}
