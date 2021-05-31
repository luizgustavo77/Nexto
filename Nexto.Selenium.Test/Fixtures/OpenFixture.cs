using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.IO;
using System.Reflection;

namespace Nexto.Selenium.Fixtures
{
    public class OpenFixture : IDisposable
    {
        public IWebDriver Driver { get; private set; }

        //Setup
        public OpenFixture()
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Driver = new FirefoxDriver(path);
        }

        //TearDown
        public void Dispose()
        {
            Driver.Quit();
        }
    }
}
