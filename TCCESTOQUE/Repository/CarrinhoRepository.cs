using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TCCESTOQUE.Data;
using TCCESTOQUE.Interfaces.Repository;
using TCCESTOQUE.Models;

namespace TCCESTOQUE.Repository
{
    public class CarrinhoRepository : BaseRepository<CarrinhoModel>, ICarrinhoRepository
    {
        public CarrinhoRepository(TCCESTOQUEContext context) : base(context)
        {

        }

        public override CarrinhoModel GetOne(Guid? id)
        {
            var carrinhoModel = _context.CarrinhoModel
                .Include(c => c.Vendedor)
                .Include(i => i.Itens)
                .ThenInclude(v => v.Produto)
                .FirstOrDefault(m => m.VendedorId == id);

            return carrinhoModel;
        }

        public CarrinhoModel GetOneByVendedorId(Guid? id)
        {
            var carrinhoModel = _context.CarrinhoModel
                .Include(c => c.Vendedor)
                .Include(i => i.Itens)
                .FirstOrDefault(m => m.VendedorId == id);

            return carrinhoModel;
        }

        public ICollection<CarrinhoModel> GetAll(Guid vendedorId)
        {
            throw new NotImplementedException();
        }
    }
}
