using Commom.Dto;
using Commom.Dto.Core;
using Commom.Proxy;
using Nexto.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Commom.Dto.Solicitacao;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Nexto.Web.Controllers
{
    [AutorizacaoSessionAdmin]
    public class CoreController : Controller
    {
        private readonly ILogger<CoreController> _logger;

        public CoreController(ILogger<CoreController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            List<UserDto> users = await new APIUser(bool.Parse(AppSettings.Get("ambienteTeste"))).GetAll();

            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Login,PassWord,PassWordConfirm,BirthDate,Telefone,Email,CPF,Sexo,Estado,Cidade,Perfil")] UserDto user)
        {
            if (user == null || string.IsNullOrEmpty(user.PassWord) || !user.PassWord.Equals(user.PassWordConfirm))
            {
                ModelState.AddModelError("PassWordConfirm", "Senhas diferentes!");
            }

            else if (ModelState.IsValid)
            {
                //user.Id = Guid.NewGuid();
                user.Perfil = 1;
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Login,PassWord,PassWordConfirm,BirthDate,Telefone,Email,CPF,Sexo,Estado,Cidade,Perfil")] UserDto user)
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

        [HttpPost]
        public async Task<JsonResult> UploadFiles()
        {
            IList<ArquivoDto> result = new List<ArquivoDto>();
            try
            {
                foreach (var file in Request.Form.Files)
                {
                    if (file != null || file.ContentType.ToLower().StartsWith("image/"))
                    {
                        MemoryStream ms = new MemoryStream();
                        file.OpenReadStream().CopyTo(ms);
                        ArquivoDto arquivo = new ArquivoDto();
                        arquivo.Nome = file.Name;
                        arquivo.Conteudo = ms.ToArray();
                        arquivo.Extensao = file.ContentType;

                        result.Add(arquivo);
                    }
                }
            }
            catch (Exception e)
            {
            }

            return Json(result);
        }
    }
}
