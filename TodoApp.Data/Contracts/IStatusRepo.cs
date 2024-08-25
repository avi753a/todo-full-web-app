
using TodoApp.Data.Entites;

namespace TodoApp.Data.Contracts
{
    public interface IStatusRepo
    {
        public Task<Status> GetStatusAsync(int id);
        public Task<List<Status>> GetAllStatusAsync();
        public Task<bool> AddStatusAsync(Status status);
        public Task<bool> RemoveStatusAsync(int id);
        public Task<bool> UpdateStatusAsync(Status status);
        public Task<bool> IsStatusExistsAsync(int id);

    }
}
