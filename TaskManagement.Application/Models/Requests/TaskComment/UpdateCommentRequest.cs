using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.Models.Requests.TaskComment
{
    public class UpdateCommentRequest
    {
        public Guid TaskId { get; set; }

    }
}
