using TodoApp.Data.Entites;

namespace TodoApp.Data.Contracts
{
    public interface IPriorityRepo
    {
        public Task<Priority> GetPriorityAsync(int id);
        public Task<List<Priority>> GetAllPrioritiesAsync();
        public Task<bool> UpdatePriorityAsync(Priority priority);
        public Task<bool> RemovePriorityAsync(int id);
        public Task<bool> AddPrioprityAsync(Priority priority);
        public Task<bool> IsPriorityExistsAsync(int id);

    }
}
