using AutoMapper;
using TodoApp.Data.Contracts;
using TodoApp.Models;
using TodoApp.Data.Entites;
using TodoApp.Service.Contracts;
using TodoApp.Data;

namespace TodoApp.Service
{
    public class PriorityService : IPriorityService
    {
        private readonly IPriorityRepo _priorityRepo;
        private readonly ICacheService<PriorityView> _cacheService;
        private readonly IMapper _mapper;

        public PriorityService(IPriorityRepo priorityRepo, IMapper mapper, ICacheService<PriorityView> cacheService)
        {
            _priorityRepo = priorityRepo;
            _mapper = mapper;
            _cacheService = cacheService;
        }
        public async Task<ServiceResult> AddPriorityAsync(PriorityDTO PriorityDTO)
        {
            Priority priority=_mapper.Map<PriorityDTO,Priority>(PriorityDTO);
            _cacheService.DeleteCachedView();
            return new ServiceResult { IsSucceeded = true, Value = await _priorityRepo.AddPrioprityAsync(priority) };

        }
        public async Task<ServiceResult> RemovePriorityAsync(int id)
        {
            if(!await _priorityRepo.IsPriorityExistsAsync(id))
            {
                return new ServiceResult { IsSucceeded= false, Message="Priority do not exists"};
            }
            _cacheService.DeleteCachedView(id);
            var result= await _priorityRepo.RemovePriorityAsync(id);
            return result ? new ServiceResult { IsSucceeded = true, Value = true, Message = "Priority Deletion Successful" } : new ServiceResult { IsSucceeded = false, Value = false, Message = "Failed to Delete Priority" };
        }
        public async Task<ServiceResult> UpdatePriorityAsync(int id,PriorityUpdation priorityUpdation)
        {
            if (!await _priorityRepo.IsPriorityExistsAsync(id))
            {
                return new ServiceResult { IsSucceeded = false, Message = "Priority do not exists" };
            }
            _cacheService.DeleteCachedView(id);
            Priority priority =await _priorityRepo.GetPriorityAsync(id);
            priority.Colour = priorityUpdation.Colour;
            priority.Name = priorityUpdation.Name;
            priority.Value = priorityUpdation.Value;
            priority.Description = priorityUpdation.Description;

            var result= await _priorityRepo.UpdatePriorityAsync(priority);
            return result ? new ServiceResult { IsSucceeded = true, Value = true, Message = "Priority Updation Successful" } : new ServiceResult { IsSucceeded = false, Value = false, Message = "Failed to Update Priority" };
        }

        public async Task<ServiceResult> GetPriorityAsync(int id)
        {
            if (_cacheService.IsValueExists(id))
            {
                return new ServiceResult { IsSucceeded = true, Value = _cacheService.GetValue(id) };
            }
            else
            {
                if(!await _priorityRepo.IsPriorityExistsAsync(id))
                {
                    return new ServiceResult { IsSucceeded = false, Message = "Priority do not Exists" };
                }
                PriorityView priority=_mapper.Map<Priority,PriorityView>(await _priorityRepo.GetPriorityAsync(id));
                _cacheService.SetValue(id, priority);
                return new ServiceResult { IsSucceeded = true, Value = priority };
            }
        }

        public async Task<ServiceResult> GetAllPreoritiesAsync()
        {
            if (_cacheService.IsListExists())
            {
                return new ServiceResult { IsSucceeded = true, Value = _cacheService.GetList() };
            }
            else
            {
                var priorityList = await _priorityRepo.GetAllPrioritiesAsync();
                List<PriorityView> resultList = new List<PriorityView>();
                foreach (var item in priorityList)
                {
                    resultList.Add(_mapper.Map<Priority, PriorityView>(item));
                }
                _cacheService.SetList(resultList);
                return new ServiceResult { IsSucceeded = true, Value = resultList };
            }
        }
    }
}
