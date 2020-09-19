using System.ComponentModel.DataAnnotations;

namespace RedBrain.Authentication.Models.Users
{
    public class RegisterModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Mobile { get; set; }

        [Required]
        public string Tenant { get; set; }

        [Required]
        public string Password { get; set; }
    }
}