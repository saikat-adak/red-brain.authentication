using System;

namespace RedBrain.Authentication.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Tenant { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }

    public class Session
    {
        public int Id { get; set; } // session id
        public string UserId{ get; set; }
        public string Tenant { get; set; }        
        public DateTime StartedAt { get; set; }
        public DateTime EndedAt { get; set; }
        public bool IsExpired { get; set; }
        public string Token { get; set; } // we don't need to store token as we can rely on jwt cryptography to authenticate the token
    }
}