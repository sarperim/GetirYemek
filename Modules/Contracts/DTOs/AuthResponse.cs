using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.DTOs
{
    public class AuthResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; } 
        public DateTime ExpiresAt { get; set; }
        public UserDTO User { get; set; }
    }
}
