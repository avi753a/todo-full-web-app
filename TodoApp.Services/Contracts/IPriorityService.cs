using TodoApp.Data.Entites;
using TodoApp.Models;

namespace TodoApp.Service.Contracts
{
    public interface IPriorityService
    {
        public Task<ServiceResult> GetAllPreoritiesAsync();
        public Task<ServiceResult> GetPriorityAsync(int id);
        public Task<ServiceResult> AddPriorityAsync(PriorityDTO PriorityDTO);
        public Task<ServiceResult> RemovePriorityAsync(int id);
        public Task<ServiceResult> UpdatePriorityAsync(int id, PriorityUpdation priorityUpdation);
    }
}


