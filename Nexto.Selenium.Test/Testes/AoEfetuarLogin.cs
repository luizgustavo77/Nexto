using Nexto.Selenium.Fixtures;
using Nexto.Selenium.PageObjects;
using OpenQA.Selenium;
using Xunit;

namespace Nexto.Selenium.Testes
{
    [Collection("Chrome Driver")]
    public class AoEfetuarLogin
    {
        private IWebDriver driver;

        public AoEfetuarLogin(OpenFixture fixture)
        {
            driver = fixture.Driver;    
        }

        [Fact]
        public void DadoCredenciaisValidasDeveIrParaDashboard()
        {
            //arrange
            var loginPO = new LoginPO(driver);
            loginPO.Visitar();
            loginPO.PreencheFormulario("LG", "nota10");

            //act
            loginPO.SubmeteFormulario();

            //assert
            Assert.Equal("Home Page - Nexto", driver.Title);
        }

        [Fact]
        public void DadoCrendenciasInvalidasDeveContinuarLogin()
        {
            //arrange
            var loginPO = new LoginPO(driver);
            loginPO.Visitar();
            loginPO.PreencheFormulario("ERRO", "ERRO");

            //act
            loginPO.SubmeteFormulario();

            //assert
            Assert.Contains("Login - Nexto", driver.Title);
        }
    }
}
