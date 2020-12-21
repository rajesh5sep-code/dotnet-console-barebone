//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Logging.Console;
//using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using Mh.Ev.Common.UserAuthority.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using Serilog;
using Microsoft.Extensions.DependencyInjection;

namespace create_users
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //setup our DI
            //ConfigurationBuilder config = new ConfigurationBuilder();
            //IConfiguration configuration = config.Build();
            //var serviceProvider = new ServiceCollection()
            //     .AddTransient<IProcess,Process>()
            //     .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
            //     .AddLogging(cfg => cfg.AddConsole())
            //     .AddHttpClient()
            //     .AddOptions()
            //     .AddUserAuthorityAuthenticationServices(configuration)
            //     .Configure<LoggerFilterOptions>(cfg => cfg.MinLevel = LogLevel.Information)
            //     .BuildServiceProvider();

            //////configure console logging 
            //var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();
            //logger.LogInformation("Starting application");

            ////serviceProvider.GetService<IProcess>().AddUsers();
            //var builder = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json");
            //var config = builder.Build();
            var builder = new ConfigurationBuilder();
            BuildConfiguration(builder);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            Log.Logger.Information("Application Starting");

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddUserAuthorityAuthenticationServices(builder.Build());
                    services.AddTransient<IUserService, UserService>();
                })
                .UseSerilog()
                .Build();

            var svc = ActivatorUtilities.CreateInstance<UserService>(host.Services);
            svc.Run();

            host.RunAsync();


        }

        static void BuildConfiguration(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: false)
                .AddEnvironmentVariables();
        }


    }

}
