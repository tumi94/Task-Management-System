using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Task = Task_Management_System__Server_.Model.Task;

namespace Task_Management_System__Server_.Data
{
  
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
        {
        }

        //public TaskDbContext()
        //{
        //}
        public DbSet<Task> Tasks { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // Configure relationships, indexes, etc.
        //    // Add migrations and update the database
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
