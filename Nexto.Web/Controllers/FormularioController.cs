using Nexto.Commom.Dto;
using Nexto.Commom.Dto.Core;
using Nexto.Commom.Dto.SelectList;
using Nexto.Commom.Dto.Solicitacao;
using Nexto.Commom.Proxy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nexto.Web.Helpers;
using System;
using System.Threading.Tasks;

namespace Nexto.Web.Controllers
{
    [AutorizacaoSessionAdmin]
    public class FormularioController : Controller
    {
        private readonly ILogger<FormularioController> _logger;

        public FormularioController(ILogger<FormularioController> logger)
        {
            _logger = logger;
            new Select().CarregaDados();
        }

        public IActionResult Create()
        {
            ViewBag.Admin = Session.GetObject<UserDto>("usuario").Perfil == 1 ? true : false;
            ViewBag.Solicitacao = RouteData.Values["id"].ToString();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Enviado,Retorno,CampoAplicacao,FundamentosInvencao,EstadoTecnica,Problemas,Solucaoinvencao,Vantagens,DescricaoDesenhos,DescricaoInvencao,Id,Nome")] FormularioDto formulario)
        {
            UserDto usuario = Session.GetObject<UserDto>("usuario");
            ViewBag.Admin = usuario.Perfil == 1 ? true : false;

            if (ModelState.IsValid)
            {
                formulario.Id = null;
                formulario.Responsavel = usuario;
                formulario.Enviado = DateTime.Now;
                formulario.Solicitacao = int.Parse(RouteData.Values["id"].ToString());
                RetornaAcaoDto result = await new APIFormulario(bool.Parse(AppSettings.Get("ambienteTeste"))).Add(formulario);
                if (result.Retorno)
                {
                    return RedirectToAction("Index", "Solicitacao");
                }
                else
                {
                    ModelState.AddModelError("Retorno", result.Mensagem);
                }
            }

            return View(formulario);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formulario = await new APIFormulario(bool.Parse(AppSettings.Get("ambienteTeste"))).Find(id.Value);
            if (formulario == null)
            {
                return NotFound();
            }

            return View(formulario);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formulario = await new APIFormulario(bool.Parse(AppSettings.Get("ambienteTeste"))).Find(id.Value);
            if (formulario == null)
            {
                return NotFound();
            }

            return View(formulario);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await new APIFormulario(bool.Parse(AppSettings.Get("ambienteTeste"))).Delete(id);
            return RedirectToAction("Index", "Solicitacao");
        }
    }
}
