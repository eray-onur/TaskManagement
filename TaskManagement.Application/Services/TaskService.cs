using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TaskManagement.Application.Models.Requests.Task;
using TaskManagement.Application.Models.Responses.Task;
using TaskManagement.Application.Services.Abstracts;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }


        public async Task<TaskDetailsModel> GetById(Guid taskId)
        {
            var task = await _taskRepository.GetById(taskId);

            var taskDetailsModel = new TaskDetailsModel(task.Id, task.CreatedDate, task.Description, task.Status, task.Type, task.NextActionDate.HasValue ? task.NextActionDate.Value : null, task.AssignedTo);

            return taskDetailsModel;
        }

        public async Task<List<TaskDetailsModel>> GetAll()
        {
            var tasks = await _taskRepository.GetAll();
            var results = new List<TaskDetailsModel>();

            foreach(var task in tasks)
            {
                var taskDetailsModel = new TaskDetailsModel(task.Id, task.CreatedDate, task.Description, task.Status, task.Type, task.NextActionDate.HasValue ? task.NextActionDate.Value : null, task.AssignedTo);
                results.Add(taskDetailsModel);
            }

            return results;
        }

        public async Task<TaskManagement.Domain.Entities.Task?> Create(CreateTaskRequest request)
        {
            var task = new TaskManagement.Domain.Entities.Task(request.Description, Domain.Enums.TaskStatus.OPENED, request.Type, request.RequiredByDate, request.AssignedTo);

            await _taskRepository.Insert(task);

            return task;
        }

        public async Task<TaskManagement.Domain.Entities.Task?> Update(UpdateTaskRequest request)
        {
            var task = await _taskRepository.GetById(request.TaskId);

            if(task == null)
            {
                return null;
            }

            var updatedTask = new Domain.Entities.Task(request.TaskId, task.CreatedDate, request.Description, request.Status, request.Type, request.AssignedTo, request.NextActionDate);

            await _taskRepository.Update(updatedTask);
            return updatedTask;
        }

        public async Task<Guid> Remove(Guid id)
        {
            Guid deletedId = await _taskRepository.SoftDelete(id);
            return deletedId;
        }
    }
}
