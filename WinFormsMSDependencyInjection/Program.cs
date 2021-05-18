using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Linq;
using System.Windows.Forms;
using WinFormsMSDependencyInjection.utils;

namespace WinFormsMSDependencyInjection
{
    static class Program
    {
        //https://docs.simpleinjector.org/en/latest/windowsformsintegration.html

        [STAThread]
        public static void Main(string[] args)
        {
            var builder = new HostBuilder()
              .ConfigureAppConfiguration((hostingContext, config) =>   // Ensure that the app settings file is read and loaded
              {
                  var env = hostingContext.HostingEnvironment;

                  config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                  config.AddEnvironmentVariables();

                  if (args != null)
                  {
                      config.AddCommandLine(args);
                  }
              }).UseSerilog((hostingContext, loggerConfiguration) =>  // Setting up logging framework to use SeriLog
              {
                  loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
              })
              .ConfigureServices((hostingContext, services) =>  // Wiring up Interfaces to concrete definitions
              {
                  services.AddOptions();
                  services.Configure<AppConfig>(hostingContext.Configuration.GetSection("AppConfig")); // Use Options Pattern for reading sections on application config

                  var section = hostingContext.Configuration.GetSection("RelayModule");
                  services.Configure<RelayConfig>(section);

                  // Bind the correct type based on the setting in the config file
                  if (section["Type"] == "ThingOne")
                      services.AddSingleton<IThing, ThingOne>();
                  else
                      services.AddSingleton<IThing, ThingTwo>();

                  AutoRegisterWindowsForms(services);
                  services.AddSingleton<IFormOpener, FormOpener>();
              });

            var host = builder.Build();

            using (var servicesScope = host.Services.CreateScope())
            {
                var services = servicesScope.ServiceProvider;
                try
                {
                    var form = services.GetRequiredService<MainForm>();
                    Application.Run(form);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error occurred");
                }
            }
        }

        private static void AutoRegisterWindowsForms(IServiceCollection services)
        {
            var types = typeof(Program).Assembly.GetTypes().Where(t => t.BaseType == typeof(Form)).ToList();

            foreach (var type in types)
            {
                services.AddTransient(type);

            }
        }
    }
}
