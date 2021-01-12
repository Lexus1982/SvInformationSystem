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
    public class SectionRepository : AbstractRepository<Section>
    {
        public SectionRepository(AdoContext context) : base(context)
        {
        }

        protected override void Map(IDataRecord record, Section entity)
        {
            //TODO: Скорректировать механизм перобразования ParentId
            var parent = record.GetValueOrDefault<decimal?>("ParentId");

            entity.Id = int.Parse(record["Id"].ToString());
            entity.ParentId = parent == null ? null : (int?)parent;
            entity.Name = record.GetValueOrDefault<string>("Name");
            entity.TypeId = int.Parse(record["TypeId"].ToString());//record.GetValueOrDefault<int>("TypeId");
            entity.Position = record.GetValueOrDefault<string>("Position");
        }

        public Section GetById(int id)
        {
            using (var command = Context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = @"MEDIATE.dbo.spNE_GetSectionById";
                command.Parameters.Add(command.AddParameter("SECTION_CODE", id, DbType.Int32));
                return ToList(command).FirstOrDefault();
            }
        }

        public IEnumerable<Section> GetByUserId(int id, int groupId)
        {
            using (var command = Context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = @"MEDIATE.dbo.spNE_GetSectionsByUserId";
                command.Parameters.Add(command.AddParameter("GROUP_CODE", groupId, DbType.Int32));
                command.Parameters.Add(command.AddParameter("USER_CODE", id, DbType.Int32));
                return ToList(command);
            }
        }
    }
}
