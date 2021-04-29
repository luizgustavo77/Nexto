using Commom.Dto;
using Commom.Dto.Solicitacao;
using Commom.Proxy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nexto.Web.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nexto.Web.Controllers
{
    [AutorizacaoSessionAdmin]
    public class SolicitacaoController : Controller
    {
        private readonly ILogger<SolicitacaoController> _logger;

        public SolicitacaoController(ILogger<SolicitacaoController> logger)
        {
            _logger = logger;
        }
                
        public async Task<IActionResult> Index()
        {
            List<SolicitacaoDto> solicitacoes = await new APISolicitacao(bool.Parse(AppSettings.Get("ambienteTeste"))).GetAll();

            return View(solicitacoes);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Login,PassWord,PassWordConfirm,BirthDate,Telefone,Email,CPF,Sexo,Estado,Cidade,Perfil")] SolicitacaoDto solicitacao)
        {
            if (ModelState.IsValid)
            {
                //solicitacao.Id = Guid.NewGuid();
                RetornaAcaoDto result = await new APISolicitacao(bool.Parse(AppSettings.Get("ambienteTeste"))).Add(solicitacao);
                if (result.Retorno)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Login", result.Mensagem);
                }
            }
            return View(solicitacao);
        }

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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await new APISolicitacao(bool.Parse(AppSettings.Get("ambienteTeste"))).Delete(id);
            return RedirectToAction(nameof(Index));
        }
    } 
}
