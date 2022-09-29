using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories.Abstract;

namespace TaskManagement.Domain.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> FindByEmail(string email);
    }
}
