using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using TCCESTOQUE.Interfaces.Service;
using TCCESTOQUE.Models;
using TCCESTOQUE.ViewModel;

namespace TCCESTOQUE.Controllers
{
    public class FornecedorController : ControllerPai
    {
        private readonly IFornecedorService _fornecedorService;

        public FornecedorController(IFornecedorService context)
        {
            _fornecedorService = context;
        }

        [Authorize]
        public IActionResult CadastroFull()
        {
            Autenticar();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult CadastroFull(FornecedorEnderecoViewModel feviewmodel)
        {
            Autenticar();
            var res = _fornecedorService.PostCadastroFull(feviewmodel);
            if (!res.IsValid)
            {
                return View(MostrarErros(res, feviewmodel));
            }

            return RedirectToAction("Index", "Fornecedor");
        }
        // GET: Fornecedor
        [Authorize]
        public IActionResult Index()
        {
            Autenticar();
            return View(_fornecedorService.GetAll(ViewBag.usuarioId));
        }

        // GET: Fornecedor/Details/5
        [Authorize]
        public IActionResult Details(Guid? id)
        {
            Autenticar();
            return View(_fornecedorService.GetOne(id));
        }

        // GET: Fornecedor/EditFull/5
        [Authorize]
        public IActionResult EditFull(Guid? id)
        {
            Autenticar();
            var edit = _fornecedorService.GetEditFull(id);
            return View(edit);

            //if (edit == null) { 
            //    Criar uma pagina para informar que Fornecedo não existe!
            //return RedirectToAction();
            //}

        }

        // POST: Fornecedor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult EditFull(Guid id, FornecedorEnderecoViewModel feviewmodel)
        {
            Autenticar();
            var res = _fornecedorService.PutEditFull(id, feviewmodel);

            if (!res.IsValid)
                return View(MostrarErros(res, feviewmodel));

            return RedirectToAction("Index", "Fornecedor");
        }

        // GET: Fornecedor/Delete/5
        [Authorize]
        public IActionResult Delete(Guid? id)
        {
            Autenticar();
            ViewBag.FornecedorId = id;
            return View(_fornecedorService.GetOne(id));
        }

        // POST: Fornecedor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult DeleteConfirmed(FornecedorModel fornecedor)
        {
            Autenticar();
            var res = _fornecedorService.PostExclusao(fornecedor.FornecedorId);
            if (res.GetType() == typeof(bool))
            {
                ViewBag.ErroExcluir = "";
                return RedirectToAction("Index", "Fornecedor");
            }

            ViewBag.FornecedorId = fornecedor.FornecedorId;
            ViewBag.FornecedorErroExcluir = (string)res;
            return View(_fornecedorService.GetOne(fornecedor.FornecedorId));
        }
    }
}
