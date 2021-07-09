using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TCCESTOQUE.POCO;

namespace TCCESTOQUE.Service
{
    public static class SecurityService
    {
        public static VendedorLogin Autenticado(HttpContext context)
        {
            string email = "";
            var vendedorId = Guid.Empty;
            VendedorLogin res;
            if (context.User.Identity.IsAuthenticated)
            {
                string usuario = context.User.Identity.Name;

                foreach (var item in context.User.Claims)
                {
                    if (item.Type == ClaimTypes.Email)
                        email = item.Value;

                    if (item.Type == ClaimTypes.SerialNumber)
                        vendedorId = Guid.Parse(item.Value);
                }

                res = new VendedorLogin()
                {
                    Usuario = usuario,
                    Email = email,
                    VendedorId = vendedorId
                };
                return res;
            }
            return null;
        }

        public static string Criptografar(string senha)
        {
            MD5 md5Hasher = MD5.Create();

            byte[] valorCriptografado = md5Hasher.ComputeHash(Encoding.Default.GetBytes(senha));

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < valorCriptografado.Length; i++)
            {
                strBuilder.Append(valorCriptografado[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
    }
}
