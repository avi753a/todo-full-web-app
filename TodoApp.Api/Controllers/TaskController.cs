using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TodoApp.Models;
using TodoApp.Service.Contracts;

namespace TodoApp.Api.Controllers
{
    [Route("TodoApp/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("/Tasks/")]
        public async Task<ActionResult> GetAllTasks()
        {
            string userId = User.FindFirstValue(ClaimTypes.Sid) ?? throw new Exception("Invalid Authentication");
            ServiceResult result =await _taskService.GetAllTasksAsync(userId);
            return result.IsSucceeded?Ok(result.Value):BadRequest(result.Message);
        }

        [HttpGet("/Tasks/{id}")]
        public async Task<ActionResult> GetTaskById(string id)
        {
            string userId = User.FindFirstValue(ClaimTypes.Sid) ?? throw new Exception("Invalid Authentication");
            var result = await _taskService.GetTaskAsync(id, userId);
            return result.IsSucceeded ? Ok(result.Value) : BadRequest(result.Message);

        }
        [HttpGet("/Tasks/{id}/Details")]
        public async Task<ActionResult> GetTaskDetailsById(string id)
        {
            string userId = User.FindFirstValue(ClaimTypes.Sid) ?? throw new Exception("Invalid Authentication");
            var result = await _taskService.GetTaskDetailsAsync(id, userId);
            return result.IsSucceeded ? Ok(result.Value) : BadRequest(result.Message);

        }

        [HttpPost("/Tasks")]
        public async Task<ActionResult> AddTask([FromBody] TaskDTO task)
        {
            string userId = User.FindFirstValue(ClaimTypes.Sid) ?? throw new Exception("Invalid Authentication");
            var result = await _taskService.AddTaskAsync(task, userId);
            return result.IsSucceeded ? Ok(result.Value) : BadRequest(result.Message);
        }

        // PUT: api/Tasks/5
        [HttpPut("/Tasks/{id}")]
        public async Task<IActionResult> EditTask([FromBody] TaskDTO task, string id)
        {
            string userId = User.FindFirstValue(ClaimTypes.Sid) ?? throw new Exception("Invalid Authentication");
            var result=await _taskService.EditTaskAsync(task, userId, id);
            return result.IsSucceeded ? Ok(result.Value) : BadRequest(result.Message);
        }
        [HttpPost("/Tasks/TasksByDate")]
        public async Task<ActionResult> GetTasksByDate([FromBody] TaskDate taskDate)
        {
            string userId = User.FindFirstValue(ClaimTypes.Sid) ?? throw new Exception("Invalid Authentication");
            var date = DateTime.Parse(taskDate.DateValue).ToUniversalTime();
            var result=await _taskService.GetTasksByDateAsync(userId, date);
            return result.IsSucceeded ? Ok(result.Value) : BadRequest(result.Message);

        }

        [HttpDelete("/Tasks/{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            string userId = User.FindFirstValue(ClaimTypes.Sid) ?? throw new Exception("Invalid Authentication");
            var result = await _taskService.DeleteTaskAsync(id.ToString(), userId);
            return result.IsSucceeded ? Ok(result.Value) : BadRequest(result.Message);
        }
    }

}

