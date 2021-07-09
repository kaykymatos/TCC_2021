using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using TCCESTOQUE.Interfaces.Service;
using TCCESTOQUE.Models;
using TCCESTOQUE.ViewModel;

namespace TCCESTOQUE.Controllers
{
    public class ProdutoController : ControllerPai
    {
        private readonly IProdutoService _produtoService;
        private readonly ISelectListService _selectListRepository;

        public ProdutoController(IProdutoService context, ISelectListService selectListRepository)
        {
            _produtoService = context;
            _selectListRepository = selectListRepository;
        }

        // GET: Produto
        [Authorize]
        public IActionResult Index()
        {
            Autenticar();
            return View(_produtoService.GetAll(ViewBag.usuarioId));
        }

        // GET: Produto/Details/5
        [Authorize]
        public IActionResult Details(Guid? id)
        {
            Autenticar();
            return View(_produtoService.GetOne(id));
        }

        // GET: Produto/Create
        [Authorize]
        public IActionResult Create()
        {
            Autenticar();
            ViewData["UnidadeMedida"] = _produtoService.SelectUnidadeDeMedida;
            ViewData["Fornecedores"] = _selectListRepository.SelectListFornecedor("FornecedorId", "NomeFantasia", ViewBag.usuarioId);
            return View();
        }

        // POST: Produto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create(ProdutoViewModel produtoModel)
        {
            Autenticar();
            var res = _produtoService.PostCriacao(produtoModel);
            if (res.IsValid)
                return RedirectToAction("Index", "Produto");

            ViewData["UnidadeMedida"] = _produtoService.SelectUnidadeDeMedida;
            ViewData["Fornecedores"] = _selectListRepository.SelectListFornecedor("FornecedorId", "NomeFantasia", ViewBag.usuarioId);

            return View(MostrarErros(res, produtoModel));

        }

        // GET: Produto/Edit/5
        [Authorize]
        public IActionResult Edit(Guid? id)
        {
            Autenticar();
            ViewData["UnidadeMedida"] = _produtoService.SelectUnidadeDeMedida;
            return View(_produtoService.GetEdicao(id));
        }

        // POST: Produto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Edit(ProdutoModel produtoModel)
        {
            Autenticar();
            var res = _produtoService.PutEdicao(produtoModel);
            if (res.IsValid)
                return RedirectToAction("Index", "Produto");

            ViewData["UnidadeMedida"] = _produtoService.SelectUnidadeDeMedida;
            return View(MostrarErros(res, produtoModel));
        }

        // GET: Produto/Delete/5
        [Authorize]
        public IActionResult Delete(Guid? id)
        {
            Autenticar();
            return View(_produtoService.GetOne(id));
        }

        // POST: Produto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult DeleteConfirmed(ProdutoModel produto)
        {
            Autenticar();
            var res = _produtoService.PostExclusao(produto.ProdutoId);
            if (res)
                return RedirectToAction("Index", "Produto");

            ViewBag.ErroExcluir = "Não foi possivel excluir esse produto, ele está em uma venda";
            return View(_produtoService.GetOne(produto.ProdutoId));
        }
    }
}
