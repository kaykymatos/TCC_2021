using System.Collections.Generic;
using TCCESTOQUE.Models;

namespace TCCESTOQUE.Interfaces.Service
{
    public interface ICarrinhoService : IBaseService<CarrinhoModel>
    {
        public ICollection<string> Finalizar(CarrinhoModel carrinho);
    }
}
