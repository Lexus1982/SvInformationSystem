using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using NetworkEquipments.Domain.ADO;
using NetworkEquipments.Domain.Entities;
using NetworkEquipments.Domain.Extensions;
using NetworkEquipments.Domain.Repository;

namespace NetworkEquipments.Domain.Repositories
{
    public class NetworkRepository : AbstractRepository<Network>
    {
        public NetworkRepository(AdoContext context) : base(context)
        {
        }

        protected override void Map(IDataRecord record, Network entity)
        { 
            entity.Id = int.Parse(record["Id"].ToString()); 
            entity.AddressId = int.Parse(record["AddressId"].ToString());
            entity.SegmentNumber = record.GetValueOrDefault<int?>("SegmentNumber");
            entity.VlanManage = record.GetValueOrDefault<string>("VlanManage");
            entity.VlanInternet = record.GetValueOrDefault<string>("VlanInternet");
            entity.IpInterval = record.GetValueOrDefault<string>("IpInterval");
            entity.EquipmentsCount = (int)record["EquipmentsCount"];
            entity.Commentary = record.GetValueOrDefault<string>("Commentary");
            entity.UserId = int.Parse(record["UserId"].ToString());
            entity.ChangeDate = (DateTime)record["ChangeDate"];
        }

        public IEnumerable<Network> Get()
        {
            using (var command = Context.CreateCommand())
            {
                command.CommandText =
                    @"select  N.NETWORK_CODE as Id, N.ADDRESS_CODE as AddressId, N.SEGMENT_NUM as SegmentNumber, rtrim(N.VLAN_MANAGE) as VlanManage,
                                        rtrim(N.VLAN_INET) as VlanInternet, rtrim(N.IP_INTERVAL) as IpInterval,isnull(E.EQUIPMENTS_COUNT, 0) as  EquipmentsCount,
                                        rtrim(N.COMMENTARY) as Commentary, N.USER_CODE as UserId, N.DATE_CHANGE as ChangeDate
                                        from    MEDIATE..NE_NETWORKS N left join 
                                                (
                                                    select    NETWORK_CODE, count(NETWORK_CODE) as EQUIPMENTS_COUNT
                                                    from      MEDIATE..NE_NETWORK_EQUIPMENTS
                                                    group by NETWORK_CODE
                                                ) as E on E.NETWORK_CODE = N.NETWORK_CODE";
                //TODO: хранимка не возвращает данные подзапроса. Глюк не могу исправить!!! Причина неясна!!!
                //command.CommandType = CommandType.StoredProcedure;
                //command.CommandText = @"MEDIATE.dbo.spNE_GetNetworks";
                return ToList(command);
            }
        }

        public IEnumerable<Network> Search(string searchValue)
        {
            using (var command = Context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = @"MEDIATE.dbo.spNE_SearchNetworks";
                command.Parameters.Add(command.AddParameter("SEARCH_VALUE", searchValue, DbType.String));
                return ToList(command);
            }
        }

        public Network GetById(int id)
        {
            using (var command = Context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = @"MEDIATE.dbo.spNE_GetNetworkById";
                command.Parameters.Add(command.AddParameter("NETWORK_CODE", id, DbType.Int32));
                return ToList(command).FirstOrDefault();
            }
        }

        public Network GetByIpInterval(string ipInterval)
        {
            using (var command = Context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;

                command.CommandText = @"MEDIATE.dbo.spNE_GetNetworksByIpInterval";
                command.Parameters.Add(command.AddParameter("IP_INTERVAL", ipInterval, DbType.String));
                return ToList(command).FirstOrDefault();
            }
        }

        public int Create(Network entity)
        {
            using (var command = Context.CreateCommand())
            {
                var returnValue = command.AddParameter("return", entity.Id, DbType.Int32, ParameterDirection.ReturnValue);

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = @"MEDIATE.dbo.spNE_AddNetwork";
                command.Parameters.Add(command.AddParameter("ADDRESS_CODE", entity.AddressId, DbType.Int32));
                command.Parameters.Add(command.AddParameter("SEGMENT_NUM", entity.SegmentNumber, DbType.Int32));
                command.Parameters.Add(command.AddParameter("VLAN_MANAGE", entity.VlanManage, DbType.String));
                command.Parameters.Add(command.AddParameter("VLAN_INET", entity.VlanInternet, DbType.String));
                command.Parameters.Add(command.AddParameter("IP_INTERVAL", entity.IpInterval, DbType.String));
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
                command.CommandText = @"MEDIATE.dbo.spNE_DelNetwork";
                command.Parameters.Add(command.AddParameter("NETWORK_CODE", id, DbType.Int32));
                return command.ExecuteNonQuery();
            }
        }

        public int Update(Network entity)
        {
            using (var command = Context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = @"MEDIATE.dbo.spNE_UpdNetwork";
                command.Parameters.Add(command.AddParameter("NETWORK_CODE", entity.Id, DbType.Int32));
                command.Parameters.Add(command.AddParameter("ADDRESS_CODE", entity.AddressId, DbType.Int32));
                command.Parameters.Add(command.AddParameter("SEGMENT_NUM", entity.SegmentNumber, DbType.Int32));
                command.Parameters.Add(command.AddParameter("VLAN_MANAGE", entity.VlanManage, DbType.String));
                command.Parameters.Add(command.AddParameter("VLAN_INET", entity.VlanInternet, DbType.String));
                command.Parameters.Add(command.AddParameter("IP_INTERVAL", entity.IpInterval, DbType.String));
                command.Parameters.Add(command.AddParameter("COMMENTARY", entity.Commentary, DbType.String));
                command.Parameters.Add(command.AddParameter("USER_CODE", entity.UserId, DbType.Int32));
                return command.ExecuteNonQuery();
            }
        }
    }
}
