using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace RedBrain.Authentication.Helpers
{
    public class SqliteDataContext : DataContext
    {
        public SqliteDataContext(IConfiguration configuration) : base(configuration) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(Configuration.GetConnectionString("Database"));
        }
    }
}