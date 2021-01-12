using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkEquipments.Domain.Interfaces
{
    // TODO: Разобраться в необходимости интерфейса IAdoContext
    public interface IAdoContext : IDisposable
    {
        IUnitOfWork CreateUnitOfWork();
        IDbCommand CreateCommand();
    }
}
