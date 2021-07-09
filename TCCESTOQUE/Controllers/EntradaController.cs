using Microsoft.AspNetCore.Mvc;
using System;
using TCCESTOQUE.Interfaces.Service;
using TCCESTOQUE.Models;

namespace TCCESTOQUE.Controllers
{
    public class EntradaController : ControllerPai
    {
        private readonly IEntradaService _entradaService;
        private readonly ISelectListService _selectListService;
        private readonly IProdutoService _produtoService;
        public EntradaController(IEntradaService context, ISelectListService selectListService, IProdutoService produtoService)
        {
            _entradaService = context;
            _selectListService = selectListService;
            _produtoService = produtoService;
        }

        // GET: Entrada
        public IActionResult Index()
        {
            Autenticar();
            return View(_entradaService.GetAll(ViewBag.usuarioId));
        }

        // GET: Entrada/Details/5
        public IActionResult Details(Guid? id)
        {
            Autenticar();
            var entrada = _entradaService.GetOne(id);
            if (entrada == null)
                return NotFound();

            return View(entrada);
        }

        // GET: Entrada/Create
        public IActionResult Create(Guid produtoId)
        {
            Autenticar();
            if (produtoId == Guid.Empty || _produtoService.GetOne(produtoId) == null || _produtoService.GetOne(produtoId).Inativo)
                return NotFound();
            ViewData["FornecedorId"] = _selectListService.SelectListFornecedor("FornecedorId", "NomeFantasia", ViewBag.usuarioId);
            ViewData["ProdutoId"] = produtoId;
            ViewData["ProdutoNome"] = _produtoService.GetOne(produtoId).Nome;
            ViewData["Medida"] = _produtoService.GetOne(produtoId).UnidadeMedida;
            return View();
        }

        // POST: Entrada/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EntradaModel entradaModel)
        {
            Autenticar();
            if (entradaModel.FornecedorId == Guid.Empty)
                ModelState.AddModelError("FornecedorId", "Selecione um fornecedor");

            if (ModelState.IsValid && entradaModel.ProdutoId != Guid.Empty)
            {
                _entradaService.PostEntrada(entradaModel);
                return RedirectToAction(nameof(Index));
            }
            ViewData["FornecedorId"] = _selectListService.SelectListFornecedor("FornecedorId", "NomeFantasia", entradaModel.FornecedorId, ViewBag.usuarioId);
            ViewData["ProdutoId"] = entradaModel.ProdutoId;
            ViewData["ProdutoNome"] = _produtoService.GetOne(entradaModel.ProdutoId).Nome;
            ViewData["Medida"] = _produtoService.GetOne(entradaModel.ProdutoId).UnidadeMedida;
            return View(entradaModel);
        }

        // GET: Entrada/Delete/5
        public IActionResult Delete(Guid? id)
        {
            Autenticar();
            var entrada = _entradaService.GetOne(id);
            if (entrada == null)
                return NotFound();

            return View(entrada);
        }

        // POST: Entrada/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(EntradaModel entradaModel)
        {
            Autenticar();
            var saida = _entradaService.CancelarEntrada(entradaModel);
            if (saida != "")
            {
                ModelState.AddModelError("", saida);
                return View(entradaModel);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
