using System;

namespace Warner.Api.Services
{
    public interface IUnitOfWork
    {
        void SubmitChanges();
    }
}
