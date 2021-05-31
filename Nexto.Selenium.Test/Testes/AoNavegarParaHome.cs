using Nexto.Selenium.Fixtures;
using Nexto.Selenium.PageObjects;
using OpenQA.Selenium;
using Xunit;

namespace Nexto.Selenium.Testes
{
    [Collection("Chrome Driver")]
    public class AoNavegarParaHome
    {
        private IWebDriver driver;

        //Setup
        public AoNavegarParaHome(OpenFixture fixture)
        {
            driver = fixture.Driver;
        }

        [Fact]
        public void AbrirHome()
        {
            //arrange
            var homePO = new HomePO(driver);
            homePO.Visitar();

            //act

            //assert
            Assert.Equal("Home Page - Nexto", driver.Title);
        }
    }
}
