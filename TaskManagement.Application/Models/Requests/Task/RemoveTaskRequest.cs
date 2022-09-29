using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.Models.Requests.Task
{
    public class RemoveTaskRequest
    {
        public Guid TaskId { get; set; }
    }
}
