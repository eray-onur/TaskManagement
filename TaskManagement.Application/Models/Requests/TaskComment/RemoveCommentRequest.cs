using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.Models.Requests.TaskComment
{
    public class RemoveCommentRequest
    {
        public Guid TaskId { get; set; }
    }
}
