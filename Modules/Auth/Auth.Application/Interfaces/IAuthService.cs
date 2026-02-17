using Contracts.DTOs;
using Contracts.Requests.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Application.Interfaces
{
    public interface IAuthService
    {
        public Task<AuthResponse> RegisterAsync(RegisterRequest request);
        public Task<AuthResponse> LoginAsync(LoginRequest request);
        public Task<AuthResponse> RefreshToken(RefreshTokenRequest refreshTokenRequest);
    }
}
