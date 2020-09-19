using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RedBrain.Authentication.Helpers;
using RedBrain.Authentication.Services;

namespace RedBrain.Authentication.Tests
{
    public class BaseTest
    {
        readonly IHostBuilder _hostBuilder = Program.CreateHostBuilder(new string[] { });
        readonly IConfiguration _config = new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build();
        public IUserService UserService { get; protected set; }
        public IMapper Mapper { get; protected set; }
        public IOptions<AppSettings> AppSettings { get; protected set; }

        public BaseTest()
        {
            var services = _hostBuilder.Build().Services;
            var dataContext = new SqliteDataContext(_config);
            dataContext.Database.EnsureDeleted();
            dataContext.Database.EnsureCreated();
            UserService = new UserService(dataContext);
            Mapper = services.GetService(typeof(IMapper)) as IMapper; ;
            AppSettings = services.GetService(typeof(IOptions<AppSettings>)) as IOptions<AppSettings>; ;
        }
    }
}