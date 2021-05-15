using Commom.Dto;
using Commom.Dto.Core;
using Commom.Dto.SelectList;
using Commom.Proxy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nexto.Web.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nexto.Web.Controllers
{
    [AutorizacaoSessionAdmin]
    public class CoreController : Controller
    {
        private readonly ILogger<CoreController> _logger;

        public CoreController(ILogger<CoreController> logger)
        {
            _logger = logger;
            new Select().CarregaDados();
        }

        public async Task<IActionResult> Index()
        {
            List<UserDto> users = await new APIUser(bool.Parse(AppSettings.Get("ambienteTeste"))).GetAll();

            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Usuario,Senha,SenhaConfirm,BirthDate,Telefone,Email,Cpf,Sexo,Estado,Cidade,Perfil")] UserDto user)
        {
            if (user == null || string.IsNullOrEmpty(user.Senha) || !user.Senha.Equals(user.SenhaConfirm))
            {
                ModelState.AddModelError("PassWordConfirm", "Senhas diferentes!");
            }

            else if (ModelState.IsValid)
            {
                RetornaAcaoDto result = await new APIUser(bool.Parse(AppSettings.Get("ambienteTeste"))).Add(user);
                if (result.Retorno)
                {
                    return RedirectToAction("Index", "Core");
                }
                else
                {
                    ModelState.AddModelError("Nome", result.Mensagem);
                }
            }
            return View(user);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await new APIUser(bool.Parse(AppSettings.Get("ambienteTeste"))).Find(id.Value);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await new APIUser(bool.Parse(AppSettings.Get("ambienteTeste"))).Find(id.Value);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Usuario,Senha,SenhaConfirm,BirthDate,Telefone,Email,Cpf,Sexo,Estado,Cidade,Perfil")] UserDto user)
        {
            if (id != user.Id)
            {
                return View(user);
            }
            await new APIUser(bool.Parse(AppSettings.Get("ambienteTeste"))).Edit(user);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await new APIUser(bool.Parse(AppSettings.Get("ambienteTeste"))).Find(id.Value);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await new APIUser(bool.Parse(AppSettings.Get("ambienteTeste"))).Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
