
using TodoApp.Models;

namespace TodoApp.Service.Contracts
{
    public interface IStatusService
    {
        public Task<ServiceResult> GetAllStatusAsync();
        public Task<ServiceResult> GetStatusAsync(int id);
        public Task<ServiceResult> AddStatusAsync(StatusDTO statusInput);
        public Task<ServiceResult> UpdateStatusAsync(int id, StatusUpdation statusUpdate);
        public Task<ServiceResult> RemoveStatusAsync(int id);
    }
}
