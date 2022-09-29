using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using TaskManagement.Application.Models.Requests.Task;
using TaskManagement.Application.Models.Responses.Task;
using TaskManagement.Application.Services.Abstracts;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }


        [HttpGet("GetById")]
        [AllowAnonymous]
        public async Task<TaskDetailsModel> GetById(Guid id)
        {
            return await _taskService.GetById(id);
        }

        [HttpGet("GetAll")]
        [AllowAnonymous]
        public async Task<List<TaskDetailsModel>> GetAll()
        {
            return await _taskService.GetAll();
        }

        [HttpPost("Create")]
        [AllowAnonymous]
        public async Task<IResult> Create([FromBody] CreateTaskRequest request)
        {
            var result = await _taskService.Create(request);

            return Results.Ok(result);
        }


        [HttpPut("Update")]
        [AllowAnonymous]
        public async Task<IResult> Update([FromBody] UpdateTaskRequest request)
        {
            var result = await _taskService.Update(request);
            return Results.Ok(result);
        }

        [HttpDelete("Delete")]
        [AllowAnonymous]
        public async Task<IResult> Remove(Guid taskId)
        {
            var result = await _taskService.Remove(taskId);
            return Results.Ok(result);
        }
    }
}
