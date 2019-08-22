using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TaskManagement.Data.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void Update(int id, TEntity entity);
        void Delete(int id);        
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<TEntity> GetAsync(int id, CancellationToken cancellationToken = default(CancellationToken));
    }
}
