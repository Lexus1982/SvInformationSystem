using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using NetworkEquipments.Domain.ADO;
using NetworkEquipments.Domain.Entities;
using NetworkEquipments.Domain.Interfaces;
using NetworkEquipments.Domain.Repositories;

namespace NetworkEquipments.Domain.Repository
{
    public class AdoUnitOfWork : IUnitOfWork
    {
        //private DatabaseContext _context;
        private IDbTransaction _transaction;
        private readonly Action<AdoUnitOfWork> _rolledBack;
        private readonly Action<AdoUnitOfWork> _committed;

        //public IRepository<Network> _networkRepository;

        public AdoUnitOfWork(IDbTransaction transaction, Action<AdoUnitOfWork> rolledBack, Action<AdoUnitOfWork> committed)
        {
            Transaction = transaction;
            _transaction = transaction;
            _rolledBack = rolledBack;
            _committed = committed;
        }

        public IDbTransaction Transaction { get; private set; }

        public void Dispose()
        {
            if (_transaction == null)
                return;

            _transaction.Rollback();
            _transaction.Dispose();
            _rolledBack(this);
            _transaction = null;
        }

        public void SaveChanges()
        {
            if (_transaction == null)
                throw new InvalidOperationException("May not call save changes twice.");

            _transaction.Commit();
            _committed(this);
            _transaction = null;
        }
    }
}
