using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TaskManagement.Data.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> AddAsync(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity Delete(TEntity entity);        
        Task<IEnumerable<TEntity>> GetAllTaskWithUser(CancellationToken cancellationToken = default(CancellationToken));
        Task<TEntity> GetAsync(int id, CancellationToken cancellationToken = default(CancellationToken));
    }
}
