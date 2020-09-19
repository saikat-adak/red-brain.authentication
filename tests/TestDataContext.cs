using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RedBrain.Authentication.Entities;

namespace RedBrain.Authentication.Helpers
{
    public class TestDataContext : DataContext
    {
        public TestDataContext(IConfiguration configuration) : base(configuration) { }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(Configuration.GetConnectionString("Database"));
        }
    }
}