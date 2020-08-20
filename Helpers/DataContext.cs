using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RedBrain.Authentication.Entities;

namespace RedBrain.Authentication.Helpers
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("Database"));
        }

        public DbSet<User> Users { get; set; }
    }
}