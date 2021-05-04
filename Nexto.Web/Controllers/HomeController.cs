using Commom.Dto.SelectList;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nexto.Web.Models;

namespace Nexto.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            new Select().CarregaDados();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [Route("[controller]/Error/{statusCode}")]
        public IActionResult Error([FromRoute] string statusCode)
        {
            ErrorViewModel error = new ErrorViewModel();
            error.RequestId = statusCode;
            return View("Error", error);
        }
    }
}
