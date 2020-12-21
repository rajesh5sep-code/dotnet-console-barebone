using Mh.Ev.Common.UserAuthority.Client.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
namespace create_users
{
    public class Process : IProcess
    {
        private readonly ILogger<Process> _logger;
        private readonly IUserAuthorityApiClient _userAuthorityApiClient;
        public Process(ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            _logger = loggerFactory.CreateLogger<Process>();
            _userAuthorityApiClient = serviceProvider.GetService<IUserAuthorityApiClient>();
        }

        public Task AddUsers()
        {
            return null;
        }
    }
}
