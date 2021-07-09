using System;
using TCCESTOQUE.Models;

namespace TCCESTOQUE.Interfaces.Repository
{
    public interface ICarrinhoRepository : IBaseRepository<CarrinhoModel>
    {
        public CarrinhoModel GetOneByVendedorId(Guid? id);
    }
}
