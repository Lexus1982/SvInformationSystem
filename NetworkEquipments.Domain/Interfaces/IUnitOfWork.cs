using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using NetworkEquipments.Domain.Entities;

namespace NetworkEquipments.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IDbTransaction Transaction { get; }
        void SaveChanges();
    }
}
