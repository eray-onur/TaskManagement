using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories.Abstract;

namespace TaskManagement.Domain.Repositories
{
    public interface ITaskCommentRepository: IBaseRepository<TaskComment>
    {
        Task<IEnumerable<TaskComment>> GetAllCommentsByTask(Guid belongingTaskId);
    }
}
