using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories.Abstract;

using Task = TaskManagement.Domain.Entities.Task;

namespace TaskManagement.Domain.Repositories
{
    public interface ITaskRepository: IBaseRepository<Task>
    {
    }
}
