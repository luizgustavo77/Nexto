using Microsoft.AspNetCore.Mvc;
using Nexto.Commom.Dto;
using Nexto.Commom.Dto.Core;
using Nexto.Commom.Dto.SelectList;
using Nexto.Commom.Dto.Solicitacao;
using Nexto.Commom.Proxy;
using Nexto.Web.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Nexto.Web.Controllers
{
    [AutorizacaoSessionAdmin]
    public class SolicitacaoController : Controller
    {
        public SolicitacaoController()
        {
            new Select().CarregaDados();
        }

        public async Task<IActionResult> Index()
        {
            List<SolicitacaoDto> solicitacoes = await new APISolicitacao(bool.Parse(AppSettings.Get("ambienteTeste"))).GetAll();

            if (solicitacoes != null)
            {
                foreach (var item in solicitacoes)
                {
                    if (item.Nome == null)
                        item.Nome = " ";
                }
            }

            return View(solicitacoes);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Sigla,Tipo,DataInicio,DataFim,Cliente,Colaborador,Status,Formularios")] SolicitacaoDto solicitacao)
        {
            if (ModelState.IsValid)
            {
                solicitacao.DataInicio = DateTime.Now;
                solicitacao.Cliente = Session.GetObject<UserDto>("usuario");

                RetornaAcaoDto result = await new APISolicitacao(bool.Parse(AppSettings.Get("ambienteTeste"))).Add(solicitacao);
                if (result.Retorno)
                {
                    return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Sigla,Tipo,DataInicio,DataFim,Cliente,Colaborador,Status,Formularios")] SolicitacaoDto solicitacao)
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

        [HttpPost]
        public async Task<JsonResult> UploadFiles()
        {
            List<ArquivoDto> result = new List<ArquivoDto>();
            try
            {
                foreach (var file in Request.Form.Files)
                {
                    if (file != null || file.ContentType.ToLower().StartsWith("image/"))
                    {
                        MemoryStream ms = new MemoryStream();
                        file.OpenReadStream().CopyTo(ms);
                        ArquivoDto arquivo = new ArquivoDto();
                        arquivo.Nome = file.FileName;
                        //arquivo.Conteudo = ms.ToArray();
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

        [HttpPost]
        public async Task SalvarArquivo(List<ArquivoDto> arquivos, int tipo, int solicitacao)
        {
            foreach (var item in arquivos)
            {
                item.Tipo = tipo;
                item.Solicitacao = solicitacao;
            }

            await new APISolicitacao(false).AddArquivos(arquivos);
        }

        [HttpPost]
        public async Task DeletarArquivo(int? id)
        {
            await new APISolicitacao(false).DeleteArquivo(id.Value);
        }
    }
}
