using System.Security.Claims;
using TCCESTOQUE.Models;

namespace TCCESTOQUE.Interfaces.Repository
{
    public interface IVendedorRepository : IBaseRepository<VendedorModel>
    {
        public ClaimsPrincipal PostLogin(VendedorModel vendedorModel);

        public VendedorModel GetByTelefone(string telefone);

        public VendedorModel GetByEmail(string email);

        public VendedorModel GetBySenha(string senha, VendedorModel vendedor);

    }
}
