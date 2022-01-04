using System;
using ItechArt.Survey.Repositories;

namespace ItechArt.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private ItechArtDbContext _dbContext;
        private CounterRepository _counterRepository;

        public CounterRepository Counters
        {
            get
            {
                if (_counterRepository == null)
                    _counterRepository = new CounterRepository(_dbContext);
                return _counterRepository;
            }
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
