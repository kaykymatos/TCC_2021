using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using TCCESTOQUE.Interfaces.Service;
using TCCESTOQUE.Models;
using TCCESTOQUE.Service;

namespace TCCESTOQUE.Controllers
{
    public class HomeController : ControllerPai
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IChartService _chartService;

        public HomeController(ILogger<HomeController> logger, IChartService chartService)
        {
            _logger = logger;
            _chartService = chartService;
        }

        public IActionResult Index()
        {
            Autenticar();
            var custo = _chartService.GetValorTotalCusto(ViewBag.usuarioId);
            var valor = _chartService.GetValorTotalValor(ViewBag.usuarioId);
            
            ViewData["custoTotal"] = custo;
            ViewData["valorTotal"] = valor-custo;
            return View();
        }

        public IActionResult Manual()
        {
            Autenticar();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
