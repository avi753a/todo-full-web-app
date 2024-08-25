
using TodoApp.Data.Entites;

namespace TodoApp.Data.Contracts
{
    public interface ITaskRepo
    {
        public Task<List<TaskItem>> GetUserAllTasksAsync(Guid userId);
        public Task<TaskItem> GetUserTaskAsync(Guid taskID);
        public Task<bool> AddTaskAsync(TaskItem taskItem);
        public Task<bool> RemoveTaskAsync(Guid taskId);
        public Task<bool> UpdateTaskAsync(TaskItem item);
        public Task<bool> IsTaskExistsAsync(Guid taskId);
        public Task<List<TaskItem>> GetUserTasksByDateAsync(Guid userId,DateTime date);

    }
}
