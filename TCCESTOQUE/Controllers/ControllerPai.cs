using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TCCESTOQUE.Service;

namespace TCCESTOQUE.Controllers
{
    public class ControllerPai : Controller
    {
        internal void Autenticar()
        {
            var autenticacao = SecurityService.Autenticado(HttpContext);
            ViewBag.usuario = autenticacao == null ? "Não Logado" : autenticacao.Usuario;
            ViewBag.email = autenticacao == null ? "Não Logado" : autenticacao.Email;
            ViewBag.usuarioId = autenticacao == null ? Guid.Empty : autenticacao.VendedorId;
            ViewBag.autenticado = autenticacao == null ? false : true;
        }

        internal object MostrarErros(ValidationResult res, object model)
        {
            if (res.Errors.Select(e => e.ErrorMessage).ToList().GetType() == typeof(List<string>))
            {
                foreach (var item in res.Errors.ToList())
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return model;
        }
    }
}
