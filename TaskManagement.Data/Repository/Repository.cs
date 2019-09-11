using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _context.AddAsync(entity);
            return entity;
        }

        public TEntity Update(TEntity entity)
        {            
            _context.Update(entity);
            return entity;
        }

        public TEntity Delete(TEntity entity)
        {            
            _context.Remove(entity);
            return entity;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var entities = await _context.Set<TEntity>().AsNoTracking().ToListAsync(cancellationToken);
            return entities;
        }

        public async Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var entity = await _context.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken);
            return entity;
        }        
    }
}
