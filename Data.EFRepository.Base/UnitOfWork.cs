using System;
using Data.Repository.Base.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.EFRepository.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public void BeginTransaction()
        {
            if (_context.Database.CurrentTransaction == null) _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            _context.Database.CommitTransaction();
        }

        public void Rollback()
        {
            _context.Database.RollbackTransaction();
        }
    }
}
