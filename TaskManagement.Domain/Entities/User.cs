using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TaskManagement.Domain.Entities.Abstract;

namespace TaskManagement.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public User(string email, string password) : base()
        {
            Email = email;
            Password = password;
        }

        public User(Guid id, string email, string password) : base()
        {
            Id = id;
            Email = email;
            Password = password;
        }
    }
}
