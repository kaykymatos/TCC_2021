using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using TCCESTOQUE.Data;
using TCCESTOQUE.Interfaces.Repository;
using TCCESTOQUE.Models;
using TCCESTOQUE.Service;

namespace TCCESTOQUE.Repository
{
    public class VendedorRepository : BaseRepository<VendedorModel>, IVendedorRepository
    {
        public VendedorRepository(TCCESTOQUEContext context) : base(context)
        {

        }

        //Apenas ADM pode acessar essa informação!!!
        public ICollection<VendedorModel> GetAll(Guid vendedorId)
        {
            return _context.VendedorModel.ToList();
        }

        public override VendedorModel GetOne(Guid? id)
        {
            var vendedorModel = _context.VendedorModel
                .FirstOrDefault(m => m.VendedorId == id);

            if (vendedorModel == null)
                return null;

            return vendedorModel;
        }

        public ClaimsPrincipal PostLogin(VendedorModel vendedor)
        {
            IList<Claim> Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, vendedor.Nome),
                new Claim(ClaimTypes.Email, vendedor.Email),
                new Claim(ClaimTypes.SerialNumber, Convert.ToString(vendedor.VendedorId))
            };

            var minhaIdentity = new ClaimsIdentity(Claims, "Vendedor");
            return new ClaimsPrincipal(new[] { minhaIdentity });
        }

        public VendedorModel GetByTelefone(string telefone)
        {
            return _context.VendedorModel.Where(a => a.Telefone == telefone).FirstOrDefault();
        }

        public VendedorModel GetByEmail(string email)
        {
            return _context.VendedorModel.Where(a => a.Email == email).FirstOrDefault();
        }

        public VendedorModel GetBySenha(string senha, VendedorModel vendedor)
        {
            if (vendedor.Logar)
                return _context.VendedorModel.Where(a => a.Senha == senha && a.VendedorId == vendedor.VendedorId).FirstOrDefault();

            return _context.VendedorModel.Where(a => a.Senha == SecurityService.Criptografar(senha) && a.Email == vendedor.Email).FirstOrDefault();
        }
    }
}
