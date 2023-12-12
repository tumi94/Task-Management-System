
using Microsoft.EntityFrameworkCore;
using System;
using Task_Management_System__Server_.Data;
using Task = Task_Management_System__Server_.Model.Task;

namespace Task_Management_System__Server_.Repositories
{
  
    public class TaskRepository
    {
        private readonly TaskDbContext _context;

        public TaskRepository(TaskDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Task> GetAllTasks()
        {
            return _context.Tasks.ToList();
        }

        public Task GetTaskById(int id)
        {
            return _context.Tasks.FirstOrDefault(t => t.Id == id);
        }

        public void AddTask(Task task)
        {
            _context.Tasks.Add(task);
        }
        public void UpdateTask(Task task)
        {
            var existingTask = _context.Tasks.FirstOrDefault(t => t.Id == task.Id);

            if (existingTask != null)
            {
                _context.Entry(existingTask).CurrentValues.SetValues(task);
                _context.Entry(existingTask).State = EntityState.Modified;
            }
        }


        public void DeleteTask(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
            }
        }

        // Implement other CRUD methods...

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }


}
