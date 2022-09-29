using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Domain.Entities.Abstract
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid();
            IsDeleted = false;
        }
    }
}
