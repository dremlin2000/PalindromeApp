using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using NLog;
using NLog.Web;

namespace Palindrome
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // NLog: setup the logger first to catch all errors
            var logger = LogManager.LoadConfiguration("nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Debug("init main");
                BuildWebHost(args).Run();
            }
            catch (Exception ex)
            {
                //NLog: catch setup errors
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureAppConfiguration((builderContext, config) => {
                    IHostingEnvironment env = builderContext.HostingEnvironment;
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables();
                })
                .ConfigureLogging(logging =>
                {
                    //logging.ClearProviders();
                    //logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                })
                .UseNLog()  // NLog: setup NLog for Dependency injection
                .Build();
    }
}