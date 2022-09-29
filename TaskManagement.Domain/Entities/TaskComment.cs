using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TaskManagement.Domain.Entities.Abstract;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Domain.Entities
{
    public class TaskComment : BaseEntity
    {
        public Guid TaskId { get; set; }
        public DateTime DateAdded { get; set; }
        public CommentType CommentType { get; set; }
        public string Comment { get; set; }
        public DateTime ReminderDate { get; set; }

        public TaskComment(Guid taskId, string comment, CommentType commentType, DateTime reminderDate) : base()
        {
            TaskId = taskId;
            Comment = comment;
            CommentType = commentType;
            ReminderDate = reminderDate;
            DateAdded = DateTime.Now;
        }

        public TaskComment(Guid id, Guid taskId, DateTime dateAdded, string comment, CommentType commentType, DateTime reminderDate) : base()
        {
            Id = id;
            TaskId = taskId;
            DateAdded= dateAdded;
            Comment = comment;
            CommentType = commentType;
            ReminderDate = reminderDate;
        }
    }
}
