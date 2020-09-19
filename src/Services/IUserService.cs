using System.Collections.Generic;
using RedBrain.Authentication.Entities;

namespace RedBrain.Authentication.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password, string tenant);
        IEnumerable<User> GetAll(string tenant);
        User GetById(int id);
        User Create(User user, string password);
        void Update(User user, string password = null);
        void Delete(int id);
    }
}