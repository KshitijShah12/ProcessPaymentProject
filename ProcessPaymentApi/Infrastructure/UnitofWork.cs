using ProcessPaymentApi.Contracts;
using ProcessPaymentApi.Entities;
using ProcessPaymentApi.Repository;
using System;

namespace ProcessPaymentApi.Infrastructure
{
    public class UnitofWork  : IUnitOfWork, IDisposable
    {
        private readonly RepositoryContext _context;

        private IRepositoryBase<ProcessPayment> _processPaymentRepository;

        private IRepositoryBase<ProcessStatus> _processStatusRepository;

        public UnitofWork(RepositoryContext context)
        {
            _context = context;
        }

        public IRepositoryBase<ProcessPayment> ProcessPaymentRepository
        {
            get { return _processPaymentRepository ?? (_processPaymentRepository = new RepositoryBase<ProcessPayment>(_context)); }
        }

        public IRepositoryBase<ProcessStatus> ProcessStatusRepository
        {
            get { return _processStatusRepository ?? (_processStatusRepository = new RepositoryBase<ProcessStatus>(_context)); }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
