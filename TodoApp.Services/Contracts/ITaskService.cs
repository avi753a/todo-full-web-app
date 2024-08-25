using TodoApp.Models;

namespace TodoApp.Service.Contracts
{
    public interface ITaskService
    {
        public Task<ServiceResult> AddTaskAsync(TaskDTO task,string userId);
        public Task<ServiceResult> GetAllTasksAsync(string id);
        public Task<ServiceResult> GetTaskAsync(string id,string userId);
        public Task<ServiceResult> GetTaskDetailsAsync(string id, string userId);
        public Task<ServiceResult> GetTasksByDateAsync(string id, DateTime date);
        public Task<ServiceResult> DeleteTaskAsync(string id,string userId);
        public Task<ServiceResult> EditTaskAsync(TaskDTO task,string userId,string id);
    }
}
