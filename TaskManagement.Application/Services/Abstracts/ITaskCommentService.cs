using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TaskManagement.Application.Models.Requests.TaskComment;
using TaskManagement.Application.Models.Responses.TaskComment;

namespace TaskManagement.Application.Services.Abstracts
{
    public interface ITaskCommentService
    {
        Task<List<TaskCommentDetailsModel>> GetCommentsByTaskId(Guid taskId);
        Task<TaskManagement.Domain.Entities.TaskComment> Create(CreateNewCommentRequest request);
        Task<TaskManagement.Domain.Entities.TaskComment> Update(UpdateCommentRequest request);
        Task<TaskManagement.Domain.Entities.TaskComment> Remove(Guid taskCommentId);
    }
}
