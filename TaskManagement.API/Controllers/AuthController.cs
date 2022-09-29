using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using TaskManagement.Application.Models.Requests.Auth;
using TaskManagement.Application.Services.Abstracts;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Authenticate")]
        [AllowAnonymous]
        public async Task<IResult> Authenticate([FromBody] AuthenticateRequest request)
        {
            var result = await _authService.Authenticate(request);
            return Results.Ok(result);
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _authService.Register(request);
            return Results.Ok(result);
        }
    }
}
