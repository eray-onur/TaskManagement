using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TaskManagement.Domain.Enums;

using TaskStatus = TaskManagement.Domain.Enums.TaskStatus;

namespace TaskManagement.Application.Models.Responses.Task
{
    public class TaskDetailsModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime RequiredByDate { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public TaskType Type { get; set; }
        public Guid? AssignedTo { get; set; }
        public DateTime NextActionDate { get; set; }

        public TaskDetailsModel(Guid id, DateTime createdDate, string description, TaskStatus status, TaskType type, DateTime nextActionDate, Guid? assignedTo = null)
        {
            Id = id;
            CreatedDate = createdDate;
            Description = description;
            Status = status;
            Type = type;
            AssignedTo = assignedTo;
            NextActionDate = nextActionDate;
        }
    }
}
