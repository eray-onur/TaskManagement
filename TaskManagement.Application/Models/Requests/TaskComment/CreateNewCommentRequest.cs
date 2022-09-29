using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.Models.Requests.TaskComment
{
    public class CreateNewCommentRequest
    {
        public Guid TaskId { get; set; }
        public CommentType CommentType { get; set; }
        public string Comment { get; set; }
        public DateTime ReminderDate { get; set; }
    }
}
