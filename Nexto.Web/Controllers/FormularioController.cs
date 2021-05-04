using Commom.Dto;
using Commom.Dto.SelectList;
using Commom.Dto.Solicitacao;
using Commom.Proxy;
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Enviado,Retorno,CampoAplicacao,FundamentosInvencao,EstadoTecnica,Problemas,Solucaoinvencao,Vantagens,DescricaoDesenhos,DescricaoInvencao,Id,Nome")] FormularioDto formulario)
        {
            if (ModelState.IsValid)
            {
                //formulario.Id = Guid.NewGuid();
                RetornaAcaoDto result = await new APIFormulario(bool.Parse(AppSettings.Get("ambienteTeste"))).Add(formulario);
                if (result.Retorno)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Login", result.Mensagem);
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
            return RedirectToAction(nameof(Index));
        }
    }
}
