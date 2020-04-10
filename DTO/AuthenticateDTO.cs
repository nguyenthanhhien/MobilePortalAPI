using System;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class AuthenticateDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string CommonServerName { get; set; }
    }
}
