using Commom.Dto;
using Commom.Dto.Core;
using Commom.Proxy;
using Nexto.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nexto.Web.Controllers
{
    public class CoreController : Controller
    {
        private readonly ILogger<CoreController> _logger;

        public CoreController(ILogger<CoreController> logger)
        {
            _logger = logger;
        }

        [AutorizacaoSessionAdmin]
        public async Task<IActionResult> Index()
        {
            List<UserDto> users = await new APIUser(bool.Parse(AppSettings.Get("ambienteTeste"))).GetAll();

            return View(users);
        }

        [AutorizacaoSessionAdmin]
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

        [AutorizacaoSessionAdmin]
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
        [AutorizacaoSessionAdmin]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Login,PassWord,PassWordConfirm,BirthDate,Telefone,Email,CPF,Sexo,Estado,Cidade,Perfil")] UserDto user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await new APIUser(bool.Parse(AppSettings.Get("ambienteTeste"))).Edit(user);

                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        [AutorizacaoSessionAdmin]
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
        [AutorizacaoSessionAdmin]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await new APIUser(bool.Parse(AppSettings.Get("ambienteTeste"))).Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
