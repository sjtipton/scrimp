using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace scrimp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((WebHostBuilderContext context, IConfigurationBuilder builder) =>
                {
                    builder.Sources.Clear();

                    builder.SetBasePath(Directory.GetCurrentDirectory())
                        .AddEnvironmentVariables(prefix: "ASPNETCORE_")
                        .AddJsonFile(GetAppSettingsFile(context.HostingEnvironment), optional: false, reloadOnChange: true)
                        .AddCommandLine(args)
                        .AddEnvironmentVariables();
                })
                .UseKestrel(o => {
                    o.ListenLocalhost(int.Parse("4000"), lo => {
                        lo.UseHttps();
                    });
                })
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();

        private static string GetAppSettingsFile(IHostingEnvironment env)
        {
            return !env.IsDevelopment() ? "appsettings.json" : "appsettings.Development.json";
        }
    }
}
