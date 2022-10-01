using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TaskManagement.Application.Models.Requests.TaskComment;
using TaskManagement.Application.Models.Responses.TaskComment;
using TaskManagement.Application.Services.Abstracts;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Services
{
    public class TaskCommentService : ITaskCommentService
    {
        private readonly ITaskCommentRepository _taskCommentRepository;
        public TaskCommentService(ITaskCommentRepository taskCommentRepository)
        {
            _taskCommentRepository = taskCommentRepository;
        }

        public async Task<List<TaskCommentDetailsModel>> GetCommentsByTaskId(Guid taskId)
        {
            var comments = await _taskCommentRepository.GetAll();

            var results = new List<TaskCommentDetailsModel>();

            foreach (var comment in comments)
            {
                var taskCommentDetailsModel = new TaskCommentDetailsModel(comment.TaskId, comment.Id, comment.Comment, comment.CommentType, comment.DateAdded, comment.ReminderDate);
                results.Add(taskCommentDetailsModel);
            }

            return results;
        }
        public async Task<Domain.Entities.TaskComment> Create(CreateNewCommentRequest request)
        {
            var taskComment = new TaskManagement.Domain.Entities.TaskComment(request.TaskId, request.Comment, request.CommentType, request.ReminderDate);

            await _taskCommentRepository.Insert(taskComment);

            return taskComment;
        }

        public async Task<Domain.Entities.TaskComment> Update(UpdateCommentRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<Domain.Entities.TaskComment> Remove(Guid taskCommentId)
        {
            throw new NotImplementedException();
        }
    }
}
