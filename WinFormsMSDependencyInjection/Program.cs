using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Windows.Forms;

namespace WinFormsMSDependencyInjection
{
    public delegate IThing ThingFactory(string key);

    static class Program
    {
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
                  {
                      services.AddSingleton<IThing, ThingOne>();
                  }
                  else
                  {
                      services.AddSingleton<IThing, ThingTwo>();
                  }

                  // Main form is bound to itself
                  services.AddScoped<MainForm>();
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
    }
}
