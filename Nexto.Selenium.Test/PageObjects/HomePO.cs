using OpenQA.Selenium;

namespace Nexto.Selenium.PageObjects
{
    public class HomePO
    {
        private IWebDriver driver;

        public HomePO(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void Visitar()
        {
            driver.Navigate().GoToUrl("https://nextoweb.azurewebsites.net/");
        }

    }
}
