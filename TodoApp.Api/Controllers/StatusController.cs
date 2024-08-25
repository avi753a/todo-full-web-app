using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TodoApp.Models;
using TodoApp.Service.Contracts;

namespace TodoApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]

    [Authorize]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _statusService;
        public StatusController(IStatusService statusService) {
         _statusService = statusService;
        }
        [HttpGet("/Status")]
        public async Task<ActionResult<List<StatusView>>> GetAllStatus()
        {
           var result=await this._statusService.GetAllStatusAsync();
            return result.IsSucceeded ? Ok(result.Value) : BadRequest(result.Message);

        }
        [HttpGet("/Status/{id}")]
        public async Task<ActionResult<StatusView>> GetStatus(int id)
        {
            var result = await _statusService.GetStatusAsync(id);
            return result.IsSucceeded ? Ok(result.Value) : BadRequest(result.Message);
        }
        [HttpPost("/Status")]
        public async Task<ActionResult<bool>> AddStatus([FromBody] StatusDTO statusDTO)
        {
            var result=await _statusService.AddStatusAsync(statusDTO);
            return result.IsSucceeded ? Ok(result.Value) : BadRequest(result.Message);
        }
        [HttpPut("/Status/{id}")]
        public async Task<ActionResult<bool>> UpdateStatus(int id,[FromBody] StatusUpdation statusUpdation)
        {
            var result=await _statusService.UpdateStatusAsync(id,statusUpdation);
            return result.IsSucceeded ? Ok(result.Value) : BadRequest(result.Message);

        }
        [HttpDelete("/Status/{id}")]
        public async Task<ActionResult<bool>> DeleteStatus(int id)
        {
           var result=await _statusService.RemoveStatusAsync(id);
            return result.IsSucceeded ? Ok(result.Value) : BadRequest(result.Message);
        }
    }
}
