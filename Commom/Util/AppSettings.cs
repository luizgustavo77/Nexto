using System.Configuration;
namespace Commom
{
    public class AppSettings
    {
        public string Get(string key)
        {
            //    System.Configuration.ConfigurationFileMap fileMap = new ConfigurationFileMap(strConfigPath);
            return ConfigurationManager.AppSettings.Get(key);
        }
    }
}
