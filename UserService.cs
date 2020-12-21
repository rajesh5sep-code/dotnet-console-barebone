//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Logging.Console;
//using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace create_users
{
    public interface IUserService
    {
        void Run();
    }
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IConfiguration _configuration;
        public UserService(ILogger<UserService> log, IConfiguration config)
        {
            _logger = log;
            _configuration = config;

        }
        public void Run()
        {
            for (int i = 0; i < _configuration.GetValue<int>("LoopTimes"); i++)
            {
                _logger.LogInformation("Run is executed {runnumber}", i);
            }

        }
    }

}
