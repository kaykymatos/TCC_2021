using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using TCCESTOQUE.Interfaces.Service;
using TCCESTOQUE.Models;

namespace TCCESTOQUE.Controllers
{
    public class VendaItensController : ControllerPai
    {
        private readonly IVendaItensService _VendaItensService;
        private readonly ISelectListService _selectListRepository;

        public VendaItensController(IVendaItensService context, ISelectListService selectListRepository)
        {
            _VendaItensService = context;
            _selectListRepository = selectListRepository;
        }

        // GET: VendaItens/Details/5
        [Authorize]
        public IActionResult Details(Guid? id)
        {
            Autenticar();
            if (id == null)
                return NotFound();

            var vendaItensModel = _VendaItensService.GetOne(id);

            if (vendaItensModel == null)
                return NotFound();

            return View(vendaItensModel);
        }

        #region ItemCarrinho
        //GET
        [Authorize]
        public IActionResult ItemCarrinho(Guid? Id)
        {
            Autenticar();
            ViewData["ProdutoId"] = _selectListRepository.SelectListProduto("ProdutoId", "Nome", ViewBag.usuarioId);
            ViewData["CarrinhoId"] = Id;
            return View();
        }


        // POST: VendaItens/Create
        [HttpPost, ActionName("ItemCarrinho")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult AdicionarItemCarrinho(VendaItensModel vendaItensModel, Guid carrinhoId)
        {
            Autenticar();
            if (vendaItensModel.CarrinhoId != carrinhoId)
                return NotFound();

            if (ModelState.IsValid)
            {
                var res = _VendaItensService.PostItem(vendaItensModel);
                if (res == "")
                    return RedirectToAction("Details", "Carrinho", new { id = vendaItensModel.VendedorId });

                ModelState.AddModelError("", res);
            }

            ViewData["ProdutoId"] = _selectListRepository.SelectListProduto("ProdutoId", "Nome", vendaItensModel.ProdutoId, ViewBag.usuarioId);
            ViewData["CarrinhoId"] = carrinhoId;
            return View();
        }

        // GET: VendaItens/Edit/5
        [Authorize]
        public IActionResult EditItemCarrinho(Guid? id)
        {
            Autenticar();
            if (id == null)
                return NotFound();

            var vendaItensModel = _VendaItensService.GetOne(id);
            if (vendaItensModel == null)
                return NotFound();

            ViewData["ProdutoId"] = _selectListRepository.SelectListProduto("ProdutoId", "Nome", vendaItensModel.ProdutoId, ViewBag.usuarioId);
            ViewData["CarrinhoId"] = vendaItensModel.CarrinhoId;
            return View(vendaItensModel);
        }

        // POST: VendaItens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult EditItemCarrinho(VendaItensModel vendaItensModel, Guid vendaItensId)
        {
            Autenticar();
            if (vendaItensId != vendaItensModel.VendaItensId)
                return NotFound();

            if (ModelState.IsValid)
            {
                var res = _VendaItensService.PutItemEdicao(vendaItensModel);
                if (res == "")
                    return RedirectToAction("Details", "Carrinho", new { id = vendaItensModel.VendedorId });

                ModelState.AddModelError("", res);
            }
            ViewData["ProdutoId"] = _selectListRepository.SelectListProduto("ProdutoId", "Nome", vendaItensModel.ProdutoId, ViewBag.usuarioId);
            ViewData["CarrinhoId"] = vendaItensModel.CarrinhoId;
            return View(_VendaItensService.GetOne(vendaItensId));
        }

        #endregion

        // GET: VendaItens/Delete/5
        [Authorize]
        public IActionResult Delete(Guid? id)
        {
            Autenticar();
            if (id == null)
                return NotFound();

            var vendaItensModel = _VendaItensService.GetOne(id);

            if (vendaItensModel == null)
                return NotFound();

            return View(vendaItensModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult DeleteItemCarrinho(VendaItensModel vendaItens)
        {
            Autenticar();
            _VendaItensService.PostItemExclusao(vendaItens.VendaItensId);
            return RedirectToAction("Details", "Carrinho", new { id = ViewBag.usuarioId });
        }
    }
}
