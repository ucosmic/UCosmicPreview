using System;

namespace UCosmic
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
    }
}