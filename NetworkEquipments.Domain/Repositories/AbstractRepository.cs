using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using NetworkEquipments.Domain.ADO;

namespace NetworkEquipments.Domain.Repository
{
    public abstract class AbstractRepository<TEntity> where TEntity : new()
    {
        AdoContext _context;

        protected AbstractRepository(AdoContext context)
        {
            _context = context;
        }

        protected AdoContext Context
        {
            get { return _context; }
        }

        protected IEnumerable<TEntity> ToList(IDbCommand command)
        {
            // TODO: Реализовать через Dapper
            // (var list = conn.Query<User>(sql, args).ToList();)
            using (var reader = command.ExecuteReader())
            {
                var items = new List<TEntity>();
                while (reader.Read())
                {
                    var item = new TEntity();
                    Map(reader, item);
                    items.Add(item);
                }
                return items;
            }
        }

        protected DataTable ToTable(IDbCommand command)
        {
            using (var reader = command.ExecuteReader())
            {
                var dataTable = new DataTable();
                dataTable.Load(reader);
                return dataTable;
            }
        }

        protected abstract void Map(IDataRecord record, TEntity entity);
    }
}
