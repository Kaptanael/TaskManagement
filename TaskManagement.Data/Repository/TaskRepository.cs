using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Data.DataContext;
using TaskManagement.Model;

namespace TaskManagement.Data.Repository
{
    public class TaskRepository :  Repository<UserTask>, ITaskRepository
    {
        public TaskRepository(TaskManagementDbContext context) : base(context)
        {

        }

        public TaskManagementDbContext TaskManagementDbContext
        {
            get { return _context as TaskManagementDbContext; }
        }
    }
}
