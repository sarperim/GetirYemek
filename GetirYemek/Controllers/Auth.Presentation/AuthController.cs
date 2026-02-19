using Auth.Application.Interfaces;
using Auth.Domain.Entities;
using Auth.Infra;
using Contracts.Events;
using Contracts.Requests.Auth;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GetirYemek.Controllers.Auth.Presentation
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


        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var response = await _authService.RegisterAsync(request);
            if(response == null)
            {
                return BadRequest(new { message = "Email is already in use or registration failed." });
            }
            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var response = await _authService.LoginAsync(request);
            if (response == null)
            {
                return Unauthorized(new { message = "Invalid email or password." });
            }
            return Ok(response);
        }

        [HttpPost("Refresh-Token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest request)
        {
            var response = await _authService.RefreshToken(request);
            if (response == null)
            {
                return Unauthorized(new { message = "Invalid or expired refresh token." });
            }
            return Ok(response);
        }
    }
}
