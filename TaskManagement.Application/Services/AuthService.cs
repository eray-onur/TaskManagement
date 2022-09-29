using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TaskManagement.Application.Models.Requests.Auth;
using TaskManagement.Application.Services.Abstracts;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<string?> Authenticate(AuthenticateRequest request)
        {
            var user = await _userRepository.FindByEmail(request.Email);

            if(user != null && (user.Email == request.Email && user.Password == request.Password))
            {
                return user.Email;
            }

            return null;
        }

        public async Task<User> Register(RegisterRequest request)
        {
            var user = new User(request.Email, request.Password);
            var result = await _userRepository.Insert(user);

            return result;
        }


    }
}
