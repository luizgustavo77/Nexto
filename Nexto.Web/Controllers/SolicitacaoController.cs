using Commom.Dto.Solicitacao;
using Commom.Proxy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nexto.Web.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nexto.Web.Controllers
{
    public class SolicitacaoController : Controller
    {
        private readonly ILogger<SolicitacaoController> _logger;

        public SolicitacaoController(ILogger<SolicitacaoController> logger)
        {
            _logger = logger;
        }

        [AutorizacaoSessionAdmin]
        public async Task<IActionResult> Index()
        {
            List<SolicitacaoDto> solicitacoes = await new APISolicitacao(bool.Parse(AppSettings.Get("ambienteTeste"))).GetAll();

            return View(solicitacoes);
        }

        [AutorizacaoSessionAdmin]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitacao = await new APISolicitacao(bool.Parse(AppSettings.Get("ambienteTeste"))).Find(id.Value);
            if (solicitacao == null)
            {
                return NotFound();
            }

            return View(solicitacao);
        }

        [AutorizacaoSessionAdmin]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitacao = await new APISolicitacao(bool.Parse(AppSettings.Get("ambienteTeste"))).Find(id.Value);
            if (solicitacao == null)
            {
                return NotFound();
            }
            return View(solicitacao);
        }

        [HttpPost]
        [AutorizacaoSessionAdmin]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Login,PassWord,PassWordConfirm,BirthDate,Telefone,Email,CPF,Sexo,Estado,Cidade,Perfil")] SolicitacaoDto solicitacao)
        {
            if (id != solicitacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await new APISolicitacao(bool.Parse(AppSettings.Get("ambienteTeste"))).Edit(solicitacao);

                return RedirectToAction(nameof(Index));
            }
            return View(solicitacao);
        }

        [AutorizacaoSessionAdmin]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitacao = await new APISolicitacao(bool.Parse(AppSettings.Get("ambienteTeste"))).Find(id.Value);
            if (solicitacao == null)
            {
                return NotFound();
            }

            return View(solicitacao);
        }

        [HttpPost, ActionName("Delete")]
        [AutorizacaoSessionAdmin]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await new APISolicitacao(bool.Parse(AppSettings.Get("ambienteTeste"))).Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
