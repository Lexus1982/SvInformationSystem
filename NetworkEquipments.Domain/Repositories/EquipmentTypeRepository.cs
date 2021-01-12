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
    public class EquipmentTypeRepository : AbstractRepository<EquipmentType>
    {
        public EquipmentTypeRepository(AdoContext context) : base(context)
        {
        }

        protected override void Map(IDataRecord record, EquipmentType entity)
        {
            entity.Id = int.Parse(record["Id"].ToString());
            entity.Name = (string)record["Name"];
            entity.Position = (int)record["Position"];
            entity.UserId = int.Parse(record["UserId"].ToString());
            entity.ChangeDate = (DateTime)record["ChangeDate"];
        }

        public IEnumerable<EquipmentType> Get()
        {
            using (var command = Context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = @"MEDIATE.dbo.spNE_GetEquipmentTypes";
                return ToList(command);
            }
        }

        public EquipmentType GetById(int id)
        {
            using (var command = Context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = @"MEDIATE.dbo.spNE_GetEquipmentTypeById";
                command.Parameters.Add(command.AddParameter("TYPE_CODE", id, DbType.Int32));
                return ToList(command).FirstOrDefault();
            }
        }

        public EquipmentType GetByName(string value)
        {
            using (var command = Context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = @"MEDIATE.dbo.spNE_GetEquipmentTypeByName";
                command.Parameters.Add(command.AddParameter("TYPE_NAME", value, DbType.String));
                return ToList(command).FirstOrDefault();
            }
        }

        public IEnumerable<EquipmentType> Search(string value)
        {
            using (var command = Context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = @"MEDIATE.dbo.spNE_SearchEquipmentTypes";
                command.Parameters.Add(command.AddParameter("SEARCH_VALUE", value, DbType.String));
                return ToList(command);
            }
        }

        public int Create(EquipmentType entity)
        {
            using (var command = Context.CreateCommand())
            {
                var returnValue = command.AddParameter("return", entity.Id, DbType.Int32, ParameterDirection.ReturnValue);

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = @"MEDIATE.dbo.spNE_AddEquipmentType";
                command.Parameters.Add(command.AddParameter("@TYPE_NAME", entity.Name, DbType.String));
                command.Parameters.Add(command.AddParameter("@POSITION", entity.Position, DbType.Int32));
                command.Parameters.Add(command.AddParameter("@USER_CODE", entity.UserId, DbType.Int32));
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
                command.CommandText = @"MEDIATE.dbo.spNE_DelEquipmentType";
                command.Parameters.Add(command.AddParameter("TYPE_CODE", id, DbType.Int32));
                return command.ExecuteNonQuery();
            }
        }

        public int Update(EquipmentType entity)
        {
            using (var command = Context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = @"MEDIATE.dbo.spNE_UpdEquipmentType";
                command.Parameters.Add(command.AddParameter("TYPE_CODE", entity.Id, DbType.Int32));
                command.Parameters.Add(command.AddParameter("TYPE_NAME", entity.Name, DbType.String));
                command.Parameters.Add(command.AddParameter("POSITION", entity.Position, DbType.Int32));
                command.Parameters.Add(command.AddParameter("USER_CODE", entity.UserId, DbType.Int32));
                return command.ExecuteNonQuery();
            }
        }
    }
}
