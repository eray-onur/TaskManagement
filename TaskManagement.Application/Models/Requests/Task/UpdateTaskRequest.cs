using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using TaskManagement.Domain.Enums;
using TaskStatus = TaskManagement.Domain.Enums.TaskStatus;

namespace TaskManagement.Application.Models.Requests.Task
{
    public class UpdateTaskRequest
    {
        public Guid TaskId { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public TaskType Type { get; set; }
        public Guid AssignedTo { get; set; }
        public DateTime NextActionDate { get; set; }
    }
}
