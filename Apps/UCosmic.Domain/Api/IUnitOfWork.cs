using System;

namespace UCosmic.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
    }
}