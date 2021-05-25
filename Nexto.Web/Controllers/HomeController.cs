using Microsoft.AspNetCore.Mvc;
using Nexto.Commom.Dto.SelectList;
using Nexto.Web.Models;

namespace Nexto.Web.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {
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
