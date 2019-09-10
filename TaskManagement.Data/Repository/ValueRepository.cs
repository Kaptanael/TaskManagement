using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskManagement.Data.DataContext;
using TaskManagement.Model;

namespace TaskManagement.Data.Repository
{
    public class ValueRepository: Repository<Value>, IValueRepository
    {
        public ValueRepository(TaskManagementDbContext context):base(context)
        {

        }

        public TaskManagementDbContext TaskManagementDbContext
        {
            get { return _context as TaskManagementDbContext; }
        }

        public async Task<bool> IsDuplicateAsync(string name, CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await _context.Values.AnyAsync(u => u.Name == name);
            return result;
        }
    }
}
