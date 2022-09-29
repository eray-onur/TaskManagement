using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TaskManagement.Domain.Repositories.Abstract
{
    public interface IBaseRepository<T>
    {
        Task<T?> GetById(Guid id);
        Task<IEnumerable<T>> GetAll();
        Task<T> Insert(T entity);
        Task<T> Update(T entity);
        Task<Guid> SoftDelete(Guid id);
    }
}
