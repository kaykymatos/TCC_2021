using System;
using TCCESTOQUE.Models;


namespace TCCESTOQUE.Interfaces.Repository
{
    public interface IFornecedorRepository : IBaseRepository<FornecedorModel>
    {
        public FornecedorModel GetByCnpj(string cnpj, Guid vendedorId);

        public FornecedorModel GetByRazaoSocial(string razao, Guid vendedorId);

        public FornecedorModel GetByNomeFantsia(string nome, Guid vendedorId);

        public FornecedorModel GetByEmail(string email, Guid vendedorId);
    }
}
