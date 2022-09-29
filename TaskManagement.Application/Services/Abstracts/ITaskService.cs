using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TaskManagement.Application.Models.Requests.Task;
using TaskManagement.Application.Models.Responses.Task;

namespace TaskManagement.Application.Services.Abstracts
{
    public interface ITaskService
    {
        Task<TaskDetailsModel> GetById(Guid taskId);
        Task<List<TaskDetailsModel>> GetAll();
        Task<TaskManagement.Domain.Entities.Task> Create(CreateTaskRequest request);
        Task<TaskManagement.Domain.Entities.Task> Update(UpdateTaskRequest request);
        Task<Guid> Remove(Guid id);
    }
}
