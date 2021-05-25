using Microsoft.AspNetCore.Mvc;
using Nexto.Web.Controllers;
using Xunit;

namespace Nexto.Test
{
    /// <summary>
    /// Caixa Preta
    /// </summary>
    public class Funcional
    {
        [Fact]
        public void ValidHome()
        {
            HomeController controller = new HomeController();
            var result = controller.Index();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void ValidCore()
        {
            CoreController controller = new CoreController();
            var result = await controller.Index();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ValidLogin()
        {
            LoginController controller = new LoginController();
            var result = controller.Index();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void ValidSolicitacao()
        {
            SolicitacaoController controller = new SolicitacaoController();
            var result = await controller.Index();

            Assert.IsType<ViewResult>(result);
        }

    }
}
