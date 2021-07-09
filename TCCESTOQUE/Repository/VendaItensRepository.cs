using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TCCESTOQUE.Data;
using TCCESTOQUE.Interfaces.Repository;
using TCCESTOQUE.Models;

namespace TCCESTOQUE.Repository
{
    public class VendaItensRepository : BaseRepository<VendaItensModel>, IVendaItensRepository
    {
        public VendaItensRepository(TCCESTOQUEContext context) : base(context)
        {

        }

        public ICollection<VendaItensModel> GetAll(Guid vendedorId)
        {
            throw new NotImplementedException();
        }

        public override VendaItensModel GetOne(Guid? id)
        {
            var vendaItensModel = _context.VendaItensModel
                .Include(v => v.Produto)
                .Include(v => v.Venda)
                .Include(c => c.Carrinho)
                .FirstOrDefault(m => m.VendaItensId == id);

            return vendaItensModel;
        }
    }
}
