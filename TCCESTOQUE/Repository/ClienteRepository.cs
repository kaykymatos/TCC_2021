using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TCCESTOQUE.Data;
using TCCESTOQUE.Interfaces.Repository;
using TCCESTOQUE.Models;

namespace TCCESTOQUE.Repository
{
    public class ClienteRepository : BaseRepository<ClienteModel>, IClienteRepository
    {
        public ClienteRepository(TCCESTOQUEContext context) : base(context)
        {

        }

        public ICollection<ClienteModel> GetAll(Guid vendedorId)
        {
            return _context.ClienteModel
                    .Include(e => e.Endereco)
                    .Where(v => v.VendedorId == vendedorId && !v.Inativo)
                    .ToList();
        }

        public override ClienteModel GetOne(Guid? id)
        {
            return _context.ClienteModel
                .Include(v => v.Venda)
                .Include(v => v.Vendedor)
                .Include(v => v.Endereco)
                .FirstOrDefault(m => m.ClienteId == id);
        }
    }
}
