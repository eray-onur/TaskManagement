using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.Models.Requests.Auth
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public RegisterRequest(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
