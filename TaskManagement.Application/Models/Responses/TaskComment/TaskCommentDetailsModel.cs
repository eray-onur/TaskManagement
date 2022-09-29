using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.Models.Responses.TaskComment
{
    public class TaskCommentDetailsModel
    {
        public Guid CommentId { get; set; }
        public Guid TaskId { get; set; }
        public DateTime DateAdded { get; set; }
        public CommentType CommentType { get; set; }
        public string Comment { get; set; }
        public DateTime ReminderDate { get; set; }

        public TaskCommentDetailsModel(Guid taskId, Guid commentId, string comment, CommentType commentType, DateTime dateAdded, DateTime reminderDate)
        {
            TaskId = taskId;
            CommentId = commentId;
            Comment = comment;
            DateAdded = dateAdded;
            CommentType = commentType;
            ReminderDate = reminderDate;
        }
    }
}
