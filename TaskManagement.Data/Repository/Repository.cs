using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskManagement.Data.DataContext;

namespace TaskManagement.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly TaskManagementDbContext _context;

        public Repository(TaskManagementDbContext context)
        {
            _context = context;
        }

        public void Add(TEntity entity)
        {
            _context.Add(entity);
        }

        public void Update(int id, TEntity entity)
        {
            var entityToUpdate = _context.Set<TEntity>().Find(id);

            if (entityToUpdate != null)
            {
                _context.Update(entity);
            }
        }

        public void Delete(int id)
        {
            var entityToDelete = _context.Set<TEntity>().Find(id);

            if (entityToDelete != null)
            {
                _context.Remove(entityToDelete);
            }
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var entities = await _context.Set<TEntity>().AsNoTracking().ToListAsync(cancellationToken);
            return entities;
        }

        public async Task<TEntity> GetAsync(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var entity = await _context.Set<TEntity>().FindAsync(id, cancellationToken);
            return entity;
        }        
    }
}
