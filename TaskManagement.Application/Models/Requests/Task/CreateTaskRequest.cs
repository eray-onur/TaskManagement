using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TaskManagement.Domain.Enums;

using TaskStatus = TaskManagement.Domain.Enums.TaskStatus;

namespace TaskManagement.Application.Models.Requests.Task
{
    public class CreateTaskRequest
    {
        public string Description { get; set; }
        public TaskType Type { get; set; }
        public Guid AssignedTo { get; set; }
        public DateTime RequiredByDate { get; set; }
    }
}
