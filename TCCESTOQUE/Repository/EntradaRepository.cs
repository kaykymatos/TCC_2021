using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TCCESTOQUE.Data;
using TCCESTOQUE.Interfaces.Repository;
using TCCESTOQUE.Models;

namespace TCCESTOQUE.Repository
{
    public class EntradaRepository : BaseRepository<EntradaModel>, IEntradaRepository
    {
        public EntradaRepository(TCCESTOQUEContext context) : base(context)
        {

        }

        public override EntradaModel GetOne(Guid? id)
        {
            return _context.EntradaModel
                .Include(p => p.Produto)
                .Include(f => f.Fornecedor)
                .Include(v => v.Vendedor)
                .FirstOrDefault(e => e.EntradaId == id);
        }

        public ICollection<EntradaModel> GetAll(Guid vendedorId)
        {
            return _context.EntradaModel
                .Include(p => p.Produto)
                .Include(f => f.Fornecedor)
                .Include(v => v.Vendedor)
                .OrderByDescending(v => v.DataEntrada)
                .Where(v => v.VendedorId == vendedorId)
                .ToList();
        }
    }
}
