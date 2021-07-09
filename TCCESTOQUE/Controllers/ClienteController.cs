using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using TCCESTOQUE.Interfaces.Service;
using TCCESTOQUE.Models;
using TCCESTOQUE.ViewModel;

namespace TCCESTOQUE.Controllers
{
    public class ClienteController : ControllerPai
    {
        private readonly IClienteService _cliService;

        public ClienteController(IClienteService context)
        {
            _cliService = context;
        }

        // GET: Cliente
        [Authorize]
        public IActionResult Index()
        {
            Autenticar();
            return View(_cliService.GetAll(ViewBag.usuarioId));
        }

        // GET: Cliente/Details/5
        [Authorize]
        public IActionResult Details(Guid? id)
        {
            Autenticar();
            if (id == null)
                return NotFound();

            var clienteModel = _cliService.GetOne(id);

            if (clienteModel == null)
                return NotFound();

            return View(clienteModel);
        }

        // GET: Cliente/Create
        [Authorize]
        public IActionResult Create()
        {
            Autenticar();
            return View();
        }

        // POST: Cliente/Create/VendedorId
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create(ClienteViewModel cliVM)
        {
            Autenticar();
            var res = _cliService.PostCriacao(cliVM);
            if (!res.IsValid)
                return View(MostrarErros(res, cliVM));

            return RedirectToAction(nameof(Index));
        }

        // GET: Cliente/Edit/5/VendedorId
        [Authorize]
        public IActionResult Edit(Guid? id)
        {
            Autenticar();
            if (id == null)
                return NotFound();

            var clienteModel = _cliService.GetEdicao(id);
            if (clienteModel == null)
                return NotFound();

            return View(clienteModel);
        }

        // POST: Cliente/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Edit(Guid id, ClienteViewModel cliVM)
        {
            Autenticar();
            if (cliVM.ClienteId == Guid.Empty)
                return NotFound();

            var res = _cliService.PutEdicao(cliVM);
            if (!res.IsValid)
                return View(MostrarErros(res, cliVM));

            return RedirectToAction(nameof(Index));
        }

        // GET: Cliente/Delete/5
        [Authorize]
        public IActionResult Delete(Guid? id)
        {
            Autenticar();
            if (id == null)
                return NotFound();

            var clienteModel = _cliService.GetOne(id);

            if (clienteModel == null)
                return NotFound();

            return View(clienteModel);
        }

        // POST: Cliente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult DeleteConfirmed(ClienteModel cliente)
        {
            Autenticar();
            var res = _cliService.PostExclusao(cliente.ClienteId);
            if (res)
                return RedirectToAction(nameof(Index));

            ViewBag.ErroExcluir = "Não é possivel deletar o cliente, ele está em alguma venda!";
            return View(_cliService.GetOne(cliente.ClienteId));
        }
    }
}
