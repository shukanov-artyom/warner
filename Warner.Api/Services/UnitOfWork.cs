using System;
using Microsoft.EntityFrameworkCore;
using Warner.Persistency;

namespace Warner.Api.Services
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        // meant to be set per HTTP request
        private readonly ApplicationDataContext dataContext;

        public UnitOfWork(DbContext dataContext)
        {
            this.dataContext = dataContext as ApplicationDataContext
                ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public void SubmitChanges()
        {
            dataContext.SaveChanges();
        }

        public void Dispose()
        {
            dataContext.Dispose();
        }
    }
}
