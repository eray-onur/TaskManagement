using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using TaskManagement.Application.Models.Requests.TaskComment;
using TaskManagement.Application.Models.Responses.TaskComment;
using TaskManagement.Application.Services.Abstracts;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskCommentController : ControllerBase
    {
        private readonly ITaskCommentService _taskCommentService;
        public TaskCommentController(ITaskCommentService taskCommentService)
        {
            _taskCommentService = taskCommentService;
        }

        [HttpGet("GetByTaskId")]
        [AllowAnonymous]
        public async Task<List<TaskCommentDetailsModel>> GetByTaskId(Guid taskId)
        {
            return await _taskCommentService.GetCommentsByTaskId(taskId);
        }

        [HttpPost("Create")]
        [AllowAnonymous]
        public async Task<IResult> Create([FromBody] CreateNewCommentRequest request)
        {
            var result = await _taskCommentService.Create(request);
            return Results.Ok(result);
        }


        [HttpPut("Update")]
        [AllowAnonymous]
        public async Task<IResult> Update(UpdateCommentRequest request)
        {
            var result = await _taskCommentService.Update(request);
            return Results.Ok(result);
        }

        [HttpDelete("Delete")]
        [AllowAnonymous]
        public async Task<IResult> Remove(Guid taskCommentId)
        {
            var result = await _taskCommentService.Remove(taskCommentId);
            return Results.Ok(result);
        }
    }
}
