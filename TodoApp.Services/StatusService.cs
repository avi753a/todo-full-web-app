
using AutoMapper;
using TodoApp.Data.Contracts;
using TodoApp.Models;
using TodoApp.Data.Entites;
using TodoApp.Service.Contracts;

namespace TodoApp.Service
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepo _statusRepo;
        private readonly IMapper _mapper;
        private readonly ICacheService<StatusView> _cacheService;

        public StatusService(IStatusRepo statusRepo, IMapper mapper, ICacheService<StatusView> cacheService)
        {
            _mapper = mapper;
            _statusRepo = statusRepo;
            _cacheService = cacheService;
        }

        public async Task<ServiceResult> GetAllStatusAsync()
        {
            if (_cacheService.IsListExists())
            {
                return new ServiceResult { IsSucceeded = true, Value = _cacheService.GetList() };
            }
            else
            {
                var statusList = await _statusRepo.GetAllStatusAsync();
                List<StatusView> resultList = new List<StatusView>();
                foreach (var statusItem in statusList)
                {
                    resultList.Add(_mapper.Map<Status, StatusView>(statusItem));
                }
                _cacheService.SetList(resultList);
                return new ServiceResult { IsSucceeded = true, Value = resultList };
            }
          
        }
        
        public async Task<ServiceResult> AddStatusAsync(StatusDTO statusInput)
        {
            var status = _mapper.Map<StatusDTO, Status>(statusInput);
            _cacheService.DeleteCachedView();
            var result= await _statusRepo.AddStatusAsync(status);
            return result ? new ServiceResult { IsSucceeded = true, Value = true, Message = "Status Creation Successful" } : new ServiceResult { IsSucceeded = false, Value = false, Message = "Failed to Create Status" };

        }
        public async Task<ServiceResult> RemoveStatusAsync(int id)
        {
            if(!await _statusRepo.IsStatusExistsAsync(id))
            {
                return new ServiceResult { IsSucceeded = false, Message = "Status do not Exist" };
            }
            _cacheService.DeleteCachedView(id);
            var result= await _statusRepo.RemoveStatusAsync(id);
            return result ? new ServiceResult { IsSucceeded = true, Value = true, Message = "Status Deletion Successful" } : new ServiceResult { IsSucceeded = false, Value = false, Message = "Failed to Delete Status" };
        }
        public async Task<ServiceResult> UpdateStatusAsync(int id,StatusUpdation statusUpdate)
        {
            if (!await _statusRepo.IsStatusExistsAsync(id))
            {
                return new ServiceResult { IsSucceeded = false, Message = "Status do not Exists" };
            }
            Status status= await _statusRepo.GetStatusAsync(id);
             status.Name=statusUpdate.Name;
            status.Description = statusUpdate.Description;
            _cacheService.DeleteCachedView(id);
            bool result= await _statusRepo.UpdateStatusAsync(status);
            return result ? new ServiceResult { IsSucceeded = true, Value = true, Message = "Status Updation Successful" } : new ServiceResult { IsSucceeded = false, Value = false, Message = "Failed to Update Status" };
        }

        public async Task<ServiceResult> GetStatusAsync(int id)
        {
            if (_cacheService.IsValueExists(id))
            {
                return new ServiceResult { IsSucceeded = true, Value = _cacheService.GetValue(id) };
            }
            else
            {
                if (!await _statusRepo.IsStatusExistsAsync(id))
                {
                    return new ServiceResult { IsSucceeded = false, Message = "Status do not exist" };
                }
                StatusView status = _mapper.Map<Status, StatusView>(await _statusRepo.GetStatusAsync(id));
                _cacheService.SetValue(id, status);
                return new ServiceResult { IsSucceeded = true, Value = status };
            }
        }
    }
}
