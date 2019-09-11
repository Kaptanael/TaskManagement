using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace TaskManagement.Data.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> AddAsync(TEntity entity);

        TEntity Update(TEntity entity);

        TEntity Delete(TEntity entity);        

        Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken = default(CancellationToken));

        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken));        
    }
}
