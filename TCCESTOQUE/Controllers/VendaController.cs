using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using TCCESTOQUE.Interfaces.Service;
using TCCESTOQUE.Models;

namespace TCCESTOQUE.Controllers
{
    public class VendaController : ControllerPai
    {
        private readonly IVendaService _vendaService;
        private readonly ISelectListService _selectListRepository;

        public VendaController(IVendaService context, ISelectListService selectListRepository)
        {
            _vendaService = context;
            _selectListRepository = selectListRepository;
        }

        // GET: Venda
        [Authorize]
        public IActionResult Index()
        {
            Autenticar();
            return View(_vendaService.GetAll(ViewBag.usuarioId));
        }

        // GET: Venda/Details/5
        [Authorize]
        public IActionResult Details(Guid? id)
        {
            Autenticar();
            if (id == null)
                return NotFound();

            var vendaModel = _vendaService.GetOne(id);

            if (vendaModel == null)
                return NotFound();

            return View(vendaModel);
        }

        // GET: Venda/Print/5
        [Authorize]
        public IActionResult Print(Guid? id)
        {
            Autenticar();
            if (id == null)
                return NotFound();

            var vendaModel = _vendaService.GetOne(id);

            if (vendaModel == null)
                return NotFound();

            return View(vendaModel);
        }

        // GET: Venda/Delete/5
        [Authorize]
        public IActionResult Delete(Guid? id)
        {
            Autenticar();
            if (id == null)
                return NotFound();

            var vendaModel = _vendaService.GetOne(id);
            if (vendaModel == null)
                return NotFound();

            return View(vendaModel);
        }

        // POST: Venda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult DeleteConfirmed(VendaModel vendaModel)
        {
            Autenticar();
            _vendaService.Cancelar(vendaModel.VendaId);
            return RedirectToAction("Index", "Venda");
        }
    }
}
