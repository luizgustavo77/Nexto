using Commom.Dto;
using Commom.Dto.Core;
using Commom.Proxy;
using Microsoft.AspNetCore.Mvc;
using Nexto.Web.Helpers;
using System;
using System.Threading.Tasks;

namespace Nexto.Web.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View("Login");
        }

        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro([Bind("Id,Nome,Login,PassWord,PassWordConfirm,BirthDate,Telefone,Email,CPF,Sexo,Estado,Cidade,Perfil")] UserDto user)
        {
            if (user == null || string.IsNullOrEmpty(user.PassWord) || !user.PassWord.Equals(user.PassWordConfirm))
            {
                ModelState.AddModelError("PassWordConfirm", "Senhas diferentes!");
            }

            else if (ModelState.IsValid)
            {
                //user.Id = Guid.NewGuid();
                user.Perfil = "Cliente";
                RetornaAcaoDto result = await new APIUser(bool.Parse(AppSettings.Get("ambienteTeste"))).Add(user);
                if (result.Retorno)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Login", result.Mensagem);
                }
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Login,PassWord")] UserDto user)
        {
            try
            {
                UserDto login = await new APIUser(bool.Parse(AppSettings.Get("ambienteTeste"))).Login(user);

                if (login != null && login.Id > 0)
                {
                    Session.Create<UserDto>("usuario", login);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("PassWord", ex.Message);
            }

            return View(user);
        }


        public async Task<IActionResult> Logout()
        {
            Session.CleanObject();
            return RedirectToAction("Index", "Home");
        }
    }
}
