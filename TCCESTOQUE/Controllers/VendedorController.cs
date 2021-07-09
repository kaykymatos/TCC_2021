using FluentValidation.Results;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using TCCESTOQUE.Interfaces.Service;
using TCCESTOQUE.Models;
using TCCESTOQUE.POCO;

namespace TCCESTOQUE.Controllers
{
    public class VendedorController : ControllerPai
    {
        private readonly IVendedorService _vendedorService;

        public VendedorController(IVendedorService vendedorService)
        {
            _vendedorService = vendedorService;
        }

        [Authorize]
        public IActionResult Index()
        {
            Autenticar();
            return View(_vendedorService.GetAll(ViewBag.usuarioId));
        }

        // GET: Vendedor/Details/5
        [Authorize]
        public IActionResult Details(Guid? id)
        {
            Autenticar();
            return View(_vendedorService.GetOne(id));
        }

        // GET: Vendedor/Create
        public IActionResult Create()
        {
            Autenticar();
            return View();
        }

        // POST: Vendedor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(VendedorModel vendedorModel)
        {
            Autenticar();
            var res = _vendedorService.PostCriacao(vendedorModel);
            if (!res.IsValid)
                return View(MostrarErros(res, vendedorModel));
            if (vendedorModel.Logar)
                Login(vendedorModel);

            return RedirectToAction("Login", "Vendedor");
        }

        // GET: Vendedor/Edit/5
        [Authorize]
        public IActionResult Edit(Guid? id)
        {
            Autenticar();
            return View(_vendedorService.GetEdicao(id));
        }

        // POST: Vendedor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Edit(Guid id, VendedorModel vendedorModel)
        {
            Autenticar();
            var res = _vendedorService.PutEdicao(id, vendedorModel);
            if (!res.IsValid)
                return View(MostrarErros(res, vendedorModel));

            return RedirectToAction("Index", "Home");
        }

        // GET: Vendedor/Delete/5
        [Authorize]
        public IActionResult Delete(Guid? id)
        {
            Autenticar();
            return View(_vendedorService.GetOne(id));
        }

        // POST: Vendedor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult DeleteConfirmed(Guid id)
        {
            Autenticar();
            var res = _vendedorService.PostExclusao(id);
            if (res)
                return RedirectToAction("Index", "Home");

            ModelState.AddModelError("", "Não foi possivel deletar sua conta, tente novamente mais tarde!");
            return View(_vendedorService.GetOne(id));
        }

        // GET
        public IActionResult Login()
        {
            Autenticar();
            return View();
        }

        //POST
        [HttpPost, ActionName("Login")]
        [ValidateAntiForgeryToken]
        public IActionResult Login(VendedorModel vendedor)
        {
            Autenticar();
            var res = _vendedorService.PostLogin(vendedor);
            if (res.GetType() == typeof(ClaimsPrincipal))
            {
                HttpContext.SignInAsync((ClaimsPrincipal)res);
                return RedirectToAction("Index", "Home");
            }
            return View(MostrarErros((ValidationResult)res, vendedor));
        }

        //GET
        [Authorize]
        public IActionResult Logout()
        {
            Autenticar();
            return View();
        }

        //POST
        [Authorize]
        [HttpPost, ActionName("Logout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogoutConfirmado()
        {
            Autenticar();
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult EsqueciSenha()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EsqueciSenha(EmailClienteModel cliente)
        {
            var res = _vendedorService.EsqueciSenha(cliente);
            if (res)
                ViewData["sucesso"] = "Verifique seu e-mail";
            else
                ViewData["erro"] = "Email não encontrado";

            return View();
        }

        [HttpGet]
        public IActionResult AlterarSenha(Guid id, Guid trocaId)
        {
            var troca = _vendedorService.GetOneAlterarSenha(trocaId);
            if (id == Guid.Empty || trocaId == Guid.Empty || troca == null || DateTime.Today > troca.DataEmissão)
                return NotFound();//Mostrar uma view falando que a url é invalida ou o código já foi utilizado!

            ViewData["VendedorId"] = id;
            ViewData["TrocaId"] = trocaId;
            return View();
        }

        [HttpPost]
        public IActionResult AlterarSenha(AlterarSenha vendedor)
        {
            var res = _vendedorService.AlterarSenha(vendedor);
            if (!res.IsValid)
            {
                ViewData["TrocaId"] = vendedor.TrocaId;
                ViewData["VendedorId"] = vendedor.VendedorId;
                return View(MostrarErros(res, vendedor));
            }

            return RedirectToAction("Login", "Vendedor");
        }

        public IActionResult AutenticarConta(Guid Id)
        {
            if (Id == null || Id == Guid.Empty)
                return NotFound();

            ViewData["ContaId"] = Id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AutenticarConta(EmailClienteModel clienteModel)
        {
            _vendedorService.AutenticarConta(clienteModel.Id);
            return RedirectToAction("Login", "Vendedor");
        }
    }
}
