using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace NetworkEquipments.Domain.Interfaces
{
    public interface IDatabaseConnectionFactory
    {
        IDbConnection Create();
    }
}
