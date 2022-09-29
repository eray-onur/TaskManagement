using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TaskManagement.Domain.Entities.Abstract;
using TaskManagement.Domain.Enums;

using TaskStatus = TaskManagement.Domain.Enums.TaskStatus;

namespace TaskManagement.Domain.Entities
{
    public class Task : BaseEntity
    {
        public DateTime CreatedDate { get; set; }
        public DateTime RequiredByDate { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public TaskType Type { get; set; }
        public Guid? AssignedTo { get; set; }
        public DateTime NextActionDate { get; set; }

        public List<TaskComment> Comments { get; set; }

        public Task(string description, TaskStatus status, TaskType type, DateTime nextActionDate, Guid? assignedTo = null) : base()
        {
            CreatedDate = DateTime.Now;

            Description = description;
            Status = status;
            Type = type;
            AssignedTo = assignedTo;
            NextActionDate = nextActionDate;
        }

        public Task(Guid id, DateTime createdDate, string description, TaskStatus status, TaskType type, Guid? assignedTo, DateTime nextActionDate) : base()
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
