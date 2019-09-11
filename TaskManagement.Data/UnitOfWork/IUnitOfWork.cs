using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskManagement.Data.Repository;

namespace TaskManagement.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        ITaskRepository Tasks { get; }
        IValueRepository Values { get; }
        int Save();
        Task<int> SaveAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
