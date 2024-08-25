using Microsoft.EntityFrameworkCore;
using TodoApp.Data.Contracts;
using TodoApp.Data.Entites;

namespace TodoApp.Data
{
    public class StatusRepo : IStatusRepo
    {
        private readonly ApplicationDbContext _context;
        public StatusRepo(ApplicationDbContext dbContext) 
        {
            _context = dbContext;
        }

      
        public async Task<List<Status>> GetAllStatusAsync()
        {
            return await _context.TaskStatuses.ToListAsync();
        }

        public async Task<Status> GetStatusAsync(int id)
        {
            var status= await _context.TaskStatuses.FirstOrDefaultAsync(s=>s.Id==id);
            return status is null ? throw new Exception("Status Not Found") : status;
        }
        public async Task<bool> AddStatusAsync(Status status)
        {
            await _context.TaskStatuses.AddAsync(status);
            return await SaveChangesAsync();
        }
        public async Task<bool> RemoveStatusAsync(int id)
        {
            Status? status=await _context.TaskStatuses.FirstOrDefaultAsync(status=>status.Id==id);
            if(status is not null) 
            {
                _context.TaskStatuses.Remove(status);
                return await SaveChangesAsync();
            }
            throw new Exception("Status Not Found");
        }
        public async Task<bool> UpdateStatusAsync(Status status)
        {
            Status? existingStatus = await _context.TaskStatuses.FirstOrDefaultAsync(s => s.Id == status.Id);
            if(existingStatus != null) 
            {
                existingStatus.Name = status.Name;
                existingStatus.Description = status.Description;
                return await SaveChangesAsync();
            }
            throw new Exception("Status Not Found");
        }
        private async Task<bool> SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IsStatusExistsAsync(int id)
        {
            return await _context.TaskStatuses.AnyAsync(status => status.Id == id);
        }
    }
}
