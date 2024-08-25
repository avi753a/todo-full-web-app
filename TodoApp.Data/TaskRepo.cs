
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TodoApp.Data.Contracts;
using TodoApp.Data.Entites;

namespace TodoApp.Data
{
    public class TaskRepo : ITaskRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public TaskRepo(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;

        }
        public async Task<bool> AddTaskAsync(TaskItem taskItem)
        {
            await _context.Tasks.AddAsync(taskItem);
            await SaveChangesAsync();
            return true;

        }
        public async Task<bool> RemoveTaskAsync(Guid id)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
            if (task is null)
            {
                throw new Exception("Task Not Found");
            }
            _context.Tasks.Remove(task);
            await SaveChangesAsync();
            return true;

        }

        public async Task<bool> UpdateTaskAsync(TaskItem item)
        {
            var existingTask = await _context.Tasks.FindAsync(item.Id);
            if (existingTask is not null)
            {
                existingTask.Name = item.Name;
                existingTask.Description = item.Description;
                existingTask.EditedTime = DateTime.UtcNow.ToUniversalTime();
                existingTask.PriorityId = item.PriorityId;
                existingTask.StatusId = item.StatusId;
                await SaveChangesAsync();
                return true;
            }
            throw new Exception("Task Not Found");


        }
        public async Task<List<TaskItem>> GetUserAllTasksAsync(Guid userId)
        {
            return await _context.Tasks
            .Include(t => t.Status)
            .Include(t => t.User)
            .Include(t => t.Priority)
            .Where(t => t.UserId == userId)
            .ToListAsync();
        }
        public async Task<TaskItem> GetUserTaskAsync(Guid taskId)
        {

            var task = await _context.Tasks
                   .Include(t => t.Status)
                   .Include(t => t.User)
                   .Include(t => t.Priority)
                   .FirstOrDefaultAsync(t => t.Id == taskId);
            if(task is null)
            {
                throw new Exception("Task Not Found");
            }
            return task;
        }

        public async Task<bool> IsTaskExistsAsync(Guid taskId)
        {
            var isTaskExists = await _context.Tasks.AnyAsync(t => t.Id == taskId);
            return isTaskExists;
        }

        public async Task<List<TaskItem>> GetUserTasksByDateAsync(Guid userId, DateTime date)
        {
            return await _context.Tasks
         .Include(t => t.Status)
         .Include(t => t.User)
         .Include(t => t.Priority)
         .Where(t => t.UserId == userId && t.CreatedTime.Date.Equals(date.Date))
         .ToListAsync();
        }
        private async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
