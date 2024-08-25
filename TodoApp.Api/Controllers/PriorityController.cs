using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Models;
using TodoApp.Service.Contracts;

namespace TodoApp.Api.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]

    [Authorize]
    public class PriorityController : ControllerBase
    {
        private readonly IPriorityService _priorityService;
        public PriorityController(IPriorityService priorityService)
        {
            _priorityService = priorityService;
        }
        [HttpGet("/Priority")]
        public async Task<ActionResult<List<PriorityView>>> GetAllPriority()
        {
            var result= await this._priorityService.GetAllPreoritiesAsync();
            return result.IsSucceeded ? Ok(result.Value) : BadRequest(result.Message);
        }
        [HttpGet("/Priority/{id}")]
        public async Task<ActionResult<PriorityView>> GetPriority(int id)
        {
            var result = await _priorityService.GetPriorityAsync(id);
            return result.IsSucceeded ? Ok(result.Value) : BadRequest(result.Message);
        }
        [HttpPost("/Priority")]
        public async Task<ActionResult<bool>> AddPriority(PriorityDTO PriorityDTO)
        {
            var result=await _priorityService.AddPriorityAsync(PriorityDTO);
            return result.IsSucceeded ? Ok(result.Value) : BadRequest(result.Message);

        }
        [HttpPut("/Priority/{id}")]
        public async Task<ActionResult<bool>> UpdatePriority(int id,[FromBody]PriorityUpdation priorityUpdation)
        {
            var result=await _priorityService.UpdatePriorityAsync(id,priorityUpdation);
            return result.IsSucceeded ? Ok(result.Value) : BadRequest(result.Message);

        }
        [HttpDelete("/Priority")]
        public async Task<ActionResult<bool>> DeletePriority(int id)
        {
            var result=await _priorityService.RemovePriorityAsync(id);
            return result.IsSucceeded ? Ok(result.Value) : BadRequest(result.Message);
        }
    }
}
