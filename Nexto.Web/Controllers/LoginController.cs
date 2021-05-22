using Commom.Dto;
using Commom.Dto.Core;
using Commom.Dto.SelectList;
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
            new Select().CarregaDados();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro([Bind("Id,Nome,Usuario,Senha,SenhaConfirm,BirthDate,Telefone,Email,Cpf,Sexo,Estado,Cidade,Perfil")] UserDto user)
        {
            if (user == null || string.IsNullOrWhiteSpace(user.Senha) || !user.Senha.Equals(user.SenhaConfirm))
            {
                ModelState.AddModelError("SenhaConfirm", "Senhas diferentes!");
                return View(user);
            }

            if (!Valid(user))
            {
                return View(user);
            }

            user.Perfil = 2;
            RetornaAcaoDto result = await new APIUser(bool.Parse(AppSettings.Get("ambienteTeste"))).Add(user);
            if (result.Retorno)
            {
                EMail.Send(user.Email, "Bem vindo ao Nexto", $"Olá! <br> esse endereço" +
                       $"de e-mail foi cadastrado no Nexto e agora você é capaz de usar " +
                       $"nossa aplicação. <br> <br> https://nextoweb.azurewebsites.net/ ");

                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("Usuario", result.Mensagem);
                return View(user);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Usuario,Senha")] UserDto user)
        {
            try
            {
                if (user == null || string.IsNullOrWhiteSpace(user.Senha))
                {
                    ModelState.AddModelError("Senha", "Senha ou usuario errado!");
                    return View(user);
                }

                UserDto result = await new APIUser(bool.Parse(AppSettings.Get("ambienteTeste"))).Login(user);

                if (result != null && result.Id > 0)
                {
                    Session.Create<UserDto>("usuario", result);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Senha", "Senha ou usuario errado!");
                    return View(user);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Senha", ex.Message);
                return View(user);
            }
        }

        [AutorizacaoSessionAdmin]
        public async Task<IActionResult> Logout()
        {
            Session.CleanObject();
            return RedirectToAction("Index", "Home");
        }

        public bool Valid(UserDto user)
        {
            bool result = true;

            if (!Test.IsValidEmail(user.Email))
            {
                result = false;
                ModelState.AddModelError("Email", "EMail invalido!");
            }
            if (!Test.IsValidCpf(user.Cpf))
            {
                result = false;
                ModelState.AddModelError("Cpf", "CPF invalido!");
            }

            return result;
        }
    }
}
