using System.ComponentModel.DataAnnotations;

namespace RedBrain.Authentication.Models.Users
{
    public class AuthenticationResultModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Tenant { get; set; }
        public string TokenType { get; set; }
        public string Token { get; set; }
    }
}