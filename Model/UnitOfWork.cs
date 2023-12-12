using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using Task_Management_System__Server_.Data;
using Task_Management_System__Server_.Interfaces;

namespace Task_Management_System__Server_.Model
{

  

    public class UnitOfWork : IUnitOfWork
    {
        private readonly TaskDbContext _context;
        private IDbContextTransaction _transaction;

        public UnitOfWork(TaskDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
            _transaction?.Commit();
        }

        public void Rollback()
        {
            try
            {
                _transaction?.Rollback();
            }
            finally
            {
                _transaction?.Dispose();
            }
        }

        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public void Dispose()
        {
            Rollback(); // Ensure any remaining transaction is rolled back on disposal
        }
    }

}
