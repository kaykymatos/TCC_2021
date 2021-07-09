using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using TCCESTOQUE.Interfaces.Service;
using TCCESTOQUE.Models;

namespace TCCESTOQUE.Controllers
{
    public class CarrinhoController : ControllerPai
    {
        private readonly ICarrinhoService _carrinhoService;
        private readonly ISelectListService _selectListService;

        public CarrinhoController(ICarrinhoService context, ISelectListService selectListService)
        {
            _carrinhoService = context;
            _selectListService = selectListService;
        }

        // GET: Carrinho/Details/5
        [Authorize]
        public IActionResult Details(Guid? id)
        {
            Autenticar();
            if (id == null || id != ViewBag.usuarioId)
                return NotFound();

            var carrinhoModel = _carrinhoService.GetOne(id);

            ViewData["ClienteId"] = _selectListService.SelectListCliente("ClienteId", "Nome", ViewBag.usuarioId);
            ViewData["CarrinhoId"] = carrinhoModel.CarrinhoId;

            if (carrinhoModel == null)
                return NotFound();

            return View(carrinhoModel);
        }

        [HttpPost, ActionName("Details")]
        [Authorize]
        public IActionResult AdicionarVenda(CarrinhoModel carrinho)
        {
            Autenticar();
            var car = _carrinhoService.Finalizar(carrinho);
            if (car.Any())
            {
                foreach (var item in car)
                {
                    ModelState.AddModelError("", item);
                }
                ViewData["ClienteId"] = _selectListService.SelectListCliente("ClienteId", "Nome", ViewBag.usuarioId);
                ViewData["CarrinhoId"] = carrinho.CarrinhoId;
                return View(_carrinhoService.GetOne(carrinho.VendedorId));
            }
            return RedirectToAction("Index", "Venda");
        }
    }
}
