using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TaskManagement.Application.Models.Requests.Auth;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Services.Abstracts
{
    public interface IAuthService
    {
        public Task<string?> Authenticate(AuthenticateRequest request);
        public Task<User> Register(RegisterRequest request);
    }
}
