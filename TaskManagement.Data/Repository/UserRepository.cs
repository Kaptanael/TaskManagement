using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Data.DataContext;
using TaskManagement.Model;

namespace TaskManagement.Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(TaskManagementDbContext context) : base(context)
        {

        }

        public TaskManagementDbContext TaskManagementDbContext
        {
            get { return _context as TaskManagementDbContext; }
        }
    }
}
