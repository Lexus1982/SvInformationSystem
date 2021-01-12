using System.Collections.Generic;
using System.Data;
using System.Linq;
using NetworkEquipments.Domain.ADO;
using NetworkEquipments.Domain.Entities;
using NetworkEquipments.Domain.Repository;
using NetworkEquipments.Domain.Extensions;

namespace NetworkEquipments.Domain.Repositories
{
    public class UserRepository : AbstractRepository<User>
    {
        public UserRepository(AdoContext context) : base(context)
        {
        }

        protected override void Map(IDataRecord record, User entity)
        {
            // TODO: Ключи в СУБД хранятся в numeric и представлены типом decimal. Возможно кастинг в int излишний.
            entity.Id = int.Parse(record["Id"].ToString());
            entity.Login = record["Login"].ToString();
            entity.Password = record.GetValueOrDefault<string>("Password");
            entity.Name = record.GetValueOrDefault<string>("Name");
            entity.GroupId = record.GetValueOrDefault<decimal>("GroupId");
            entity.Active = record.GetValueOrDefault<string>("Active");
        }

        public IEnumerable<User> Get()
        {
            using (var command = Context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = @"MEDIATE.dbo.spNE_GetUsers";
                return ToList(command);
            }
        }

        public User FindByLogin(string value)
        {
            using (var command = Context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = @"MEDIATE.dbo.spNE_FindUserByLogin";
                command.Parameters.Add(command.AddParameter("USER_NAME", value, DbType.String));
                return ToList(command).FirstOrDefault();
            }
        }
    }
}
