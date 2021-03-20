using Microsoft.Extensions.Configuration;
using System.IO;

namespace Nexto.Web.Helpers
{
    public static class AppSettings
    {
        public static string Get(string key)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
            IConfigurationRoot Configuration = builder.Build();
            return Configuration[key];
        }
    }
}