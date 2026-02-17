using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Contracts.Requests.Auth
{
    public class AddAddressRequest
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; } = string.Empty; // e.g. "Home", "Work"

        [Required]
        [MaxLength(100)]
        public string City { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Street { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string FullAddress { get; set; } = string.Empty;
    }
}
