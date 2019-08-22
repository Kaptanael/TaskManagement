using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TaskManagement.Model;

namespace TaskManagement.Data.DataContext
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TaskManagementDbContext>
    {        
        public TaskManagementDbContext CreateDbContext(string[] args)
        {
            //IConfigurationRoot configuration = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json")
            //    .Build();
            var builder = new DbContextOptionsBuilder<TaskManagementDbContext>();
            var connectionString = @"Data Source=(local);Initial Catalog=TaskManagementDb;Integrated Security=True;MultipleActiveResultSets=true;";//configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);
            return new TaskManagementDbContext(builder.Options);
        }        
    }
}
