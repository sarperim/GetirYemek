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
            return Ok(response);
        }
    }
}
