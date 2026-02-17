using Auth.Application.Interfaces;
using Auth.Application.Interfaces.Repository;
using Contracts.DTOs;
using Contracts.Requests.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Application.Services
{
    public class AuthService : IAuthService
    {
        public readonly IEventBus _EventBus;
        public readonly IUserRepository _UserRepository;
        public AuthService(IEventBus eventBus, IUserRepository userRepository)
        {
            _EventBus = eventBus; 
            _UserRepository = userRepository;
        }


        public Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AuthResponse> RefreshToken(RefreshTokenRequest refreshTokenRequest)
        {
            throw new NotImplementedException();
        }

        public Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
