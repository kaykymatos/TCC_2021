using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TCCESTOQUE.Data;
using TCCESTOQUE.Interfaces.Repository;
using TCCESTOQUE.Models;

namespace TCCESTOQUE.Repository
{
    public class VendaRepository : BaseRepository<VendaModel>, IVendaRepository
    {

        public VendaRepository(TCCESTOQUEContext context) : base(context)
        {

        }

        public override VendaModel GetOne(Guid? id)
        {
            var vendaModel = _context.VendaModel
                .Include(v => v.Cliente)
                .Include(v => v.Vendedor)
                .Include(v => v.Itens)
                .ThenInclude(p => p.Produto)
                .FirstOrDefault(m => m.VendaId == id);

            return vendaModel;
        }

        public ICollection<VendaModel> GetAll(Guid vendedorId)
        {
            return _context.VendaModel.Include(v => v.Cliente)
                .Include(v => v.Vendedor)
                .Include(i => i.Itens)
                .ThenInclude(e => e.Produto)
                .Where(v => v.VendedorId == vendedorId)
                .OrderByDescending(d => d.DataVenda)
                .ToList();
        }
    }
}
