using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TodoApp.Data.Contracts;
using TodoApp.Data.Entites;
using TodoApp.Models;
using TodoApp.Service.Contracts;

namespace TodoApp.Service
{
    public class TaskService : ITaskService
    {
        private readonly UserManager<User> _userManager;
        private readonly ITaskRepo _taskRepo;
        private readonly IStatusRepo _statusRepo;
        private readonly IPriorityRepo _priorityRepo;
        private readonly IMapper _mapper;
        public TaskService(UserManager<User> userManager, IPriorityRepo priorityRepo, ITaskRepo taskRepo, IStatusRepo statusRepo, IMapper mapper)
        {
            _userManager = userManager;
            _taskRepo = taskRepo;
            _statusRepo = statusRepo;
            _priorityRepo = priorityRepo;
            _mapper = mapper;

        }
        public async Task<ServiceResult> AddTaskAsync(TaskDTO input, string userId)
        {
            var userGuid = new Guid(userId);
            TaskItem newTask = _mapper.Map<TaskDTO, TaskItem>(input);
            newTask.UserId=userGuid;
           var result= await _taskRepo.AddTaskAsync(newTask);
            return new ServiceResult { IsSucceeded = result, Value = result };
         
        }
        public async Task<ServiceResult> GetAllTasksAsync(string userId)
        {
            var userGuid = new Guid(userId);
            List<TaskItem> taskList = await _taskRepo.GetUserAllTasksAsync(userGuid);
            List<TaskView> viewList = [];
            foreach (var a in taskList)
            {
                viewList.Add(_mapper.Map<TaskItem, TaskView>(a));
            }
            return new ServiceResult { IsSucceeded= true, Value = viewList,Message=viewList.Count>0?"No tasks Exists":"Tasks Found" };
            
        }
        public async Task<ServiceResult> GetTaskAsync(string id, string userId)
        {
            var taskId = new Guid(id);
            var userGuid = new Guid(userId);
            if (await _taskRepo.IsTaskExistsAsync(taskId))
            {
                var task =_mapper.Map<TaskItem, TaskView>(await _taskRepo.GetUserTaskAsync(taskId));
                return new ServiceResult { IsSucceeded=true,Value= task };
            }
            return new ServiceResult { IsSucceeded=false,Message="Task Not Found",Value=id};
        }
        public async Task<ServiceResult> DeleteTaskAsync(string id, string userId)
        {
            var taskId = new Guid(id);
            var userGuid = new Guid(userId);
            if (await _taskRepo.IsTaskExistsAsync(taskId))
            {
                var task= await _taskRepo.RemoveTaskAsync(taskId);
                return new ServiceResult { IsSucceeded = true, Value = id };
            }
            return new ServiceResult { IsSucceeded=false,Value=id,Message="Task do not exists"};

        }
        public async Task<ServiceResult> EditTaskAsync(TaskDTO input, string userId, string id)
        {
            var userGuid = new Guid(userId);
            var taskId = new Guid(id);
            TaskItem? existingTask;
            if (await _taskRepo.IsTaskExistsAsync(taskId))
            {
                existingTask = await _taskRepo.GetUserTaskAsync(taskId);
                if(existingTask is null)
                {
                    return new ServiceResult { IsSucceeded=false,Message="Task Updation Failed",Value=taskId};
                }
                var taskItem = new TaskItem
                {
                    Id = existingTask.Id,
                    Description = input.Description,
                    Name = input.Name,
                    StatusId = input.StatusId,
                    PriorityId = input.PriorityId,
                    CreatedTime = existingTask.CreatedTime,
                    EditedTime = DateTime.UtcNow.ToUniversalTime(),
                    UserId = userGuid
                };
                bool result= await _taskRepo.UpdateTaskAsync(taskItem);
                return new ServiceResult { IsSucceeded = result, Value = result };
              
            }
            return new ServiceResult { IsSucceeded = false, Message = "Task do not Exists", Value = taskId };
        }
        public async Task<ServiceResult> GetTasksByDateAsync(string userId,DateTime dateInput)
        {
            
                Guid userGUID = new Guid(userId);
                var taskList=await _taskRepo.GetUserTasksByDateAsync(userGUID, dateInput);
                List<TaskView> viewList = [];
                foreach (var a in taskList)
                {
                    viewList.Add(_mapper.Map<TaskItem, TaskView>(a));
                }
                return new ServiceResult { IsSucceeded=true,Value=viewList};           
            
        } 
        public async Task<ServiceResult> GetTaskDetailsAsync(string id,string userId)
        {
            var taskId = new Guid(id);
            var userGuid = new Guid(userId);
            if (await _taskRepo.IsTaskExistsAsync(taskId))
            {
                var task = _mapper.Map<TaskItem,TaskDetails>(await _taskRepo.GetUserTaskAsync(taskId));
                return new ServiceResult { IsSucceeded=true , Value = task ,Message="Task Found"};
            }
            return new ServiceResult { IsSucceeded = false, Message = "Task do not Exists", Value = taskId };
        }


    }
}
