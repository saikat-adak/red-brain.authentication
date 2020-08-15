using System.ComponentModel.DataAnnotations;

namespace RedBrain.Authentication.Models.Users
{
    public class AuthenticateModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Tenant { get; set; }
    }
}