using Microsoft.EntityFrameworkCore;
using TodoApp.Data.Contracts;
using TodoApp.Data.Entites;

namespace TodoApp.Data
{
    public class PriorityRepo : IPriorityRepo
    {
        private readonly ApplicationDbContext _context;
        public PriorityRepo(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        
        public async Task<List<Priority>> GetAllPrioritiesAsync()
        {
            return await _context.TaskPriorities.ToListAsync();
        }

        public async Task<Priority> GetPriorityAsync(int id)
        {
            var priority= await _context.TaskPriorities.FindAsync(id);
            return priority is null ? throw new Exception("Priority Not Found") : priority;

        }
        public async Task<bool> AddPrioprityAsync(Priority priority)
        {
            await _context.TaskPriorities.AddAsync(priority);
            return await SaveChangesAsync();
        }
        public async Task<bool> RemovePriorityAsync(int id)
        {
            Priority? priority = await _context.TaskPriorities.FirstOrDefaultAsync(p=>p.Id == id);
            if (priority is  null)
            {
                throw new Exception("Priority Not Found");

            }
            _context.TaskPriorities.Remove(priority);
            return await SaveChangesAsync();
        }
        public async Task<bool> IsPriorityExistsAsync(int id)
        {
            return await _context.TaskPriorities.AnyAsync(p => p.Id == id);
        }
        public async Task<bool> UpdatePriorityAsync(Priority priority)
        {
            Priority? existingPriority=_context.TaskPriorities.FirstOrDefault(p => p.Id == priority.Id);
            if(existingPriority != null) 
            {
                existingPriority.Name= priority.Name;
                existingPriority.Description= priority.Description;
                existingPriority.Value= priority.Value;
                existingPriority.Colour= priority.Colour;
                return await SaveChangesAsync();
            }
            throw new Exception("Priority Not Found");
        }
        private async Task<bool> SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
