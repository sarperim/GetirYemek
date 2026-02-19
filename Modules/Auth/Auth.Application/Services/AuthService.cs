using Auth.Application.Interfaces;
using Auth.Application.Interfaces.Repository;
using Auth.Domain.Entities;
using Contracts.DTOs;
using Contracts.Events;
using Contracts.Requests.Auth;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Auth.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _uow;
        private readonly IConfiguration _config;
        
        public AuthService(IPublishEndpoint publishEndpoint, IUserRepository userRepository, IUnitOfWork unitOfWork, IConfiguration config)
        {
            _publishEndpoint = publishEndpoint;
            _userRepository = userRepository;
            _uow = unitOfWork;
            _config = config;
        }


        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if(user == null)
            {
                return null;
            }
            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password)
               == PasswordVerificationResult.Failed)
            {
                return null;
            }

            return new AuthResponse
            {
                AccessToken = CreateToken(user),
                RefreshToken = await GenerateAndSaveRefreshTokenAsync(user),
                ExpiresAt = DateTime.UtcNow.AddDays(1),
                Name = user.FirstName

            };


        }

        public async Task<AuthResponse> RefreshToken(RefreshTokenRequest request)
        {
            var user = await _userRepository.GetByRefreshTokenAsync(request.RefreshToken);
            if (user is null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return null; 
            }

            return new AuthResponse
            {
                AccessToken = CreateToken(user),
                RefreshToken = await GenerateAndSaveRefreshTokenAsync(user),
                ExpiresAt = DateTime.UtcNow.AddDays(1),
                Name = user.FirstName

            };
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            if (!await _userRepository.IsEmailUniqueAsync(request.Email))
            {
                return null;
            }
            var user = new User(request.Email, request.FirstName, request.LastName);

            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, request.Password);
            _userRepository.Add(user);
            await _publishEndpoint.Publish<IUserCreated>(new
            {
                UserId = user.Id
            });
            var refreshToken = await GenerateAndSaveRefreshTokenAsync(user); // uow saves to db here

            return new AuthResponse
            {
                AccessToken = CreateToken(user),
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(1),
                Name = user.FirstName
                

            };
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name,user.FirstName),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<string>("AppSettings:Token")));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new JwtSecurityToken(
               issuer: _config.GetValue<string>("AppSettings:Issuer"),
                audience: _config.GetValue<string>("AppSettings:Audience"),
               claims: claims,
               expires: DateTime.UtcNow.AddDays(1),
               signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private async Task<String> GenerateAndSaveRefreshTokenAsync(User user)
        {
            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(1);
            await _uow.SaveChangesAsync();
            return refreshToken;
        }
    } 
}
