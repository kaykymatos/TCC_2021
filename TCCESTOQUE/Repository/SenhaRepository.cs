using System;
using System.Collections.Generic;
using System.Linq;
using TCCESTOQUE.Data;
using TCCESTOQUE.Interfaces.Repository;
using TCCESTOQUE.Models;
using TCCESTOQUE.POCO;

namespace TCCESTOQUE.Repository
{
    public class SenhaRepository : BaseRepository<AlterarSenhaModel>, ISenhaRepository
    {
        public SenhaRepository(TCCESTOQUEContext context) : base(context)
        {

        }

        public override AlterarSenhaModel GetOne(Guid? id)
        {
            return _context.AlterarSenhaModel.Where(m => m.Id == id && !m.Invalida).FirstOrDefault();
        }

        public bool ChegarUltimaTroca(Guid? vendedorId)
        {
            var dis = _context.AlterarSenhaModel.Where(m => m.VendedorId == vendedorId && !m.Invalida && m.DataEmissão.Date == DateTime.Today).FirstOrDefault();
            if (dis == null)
                return true;

            return false;
        }

        public AlterarSenhaModel GetOneByCodigo(AlterarSenha model)
        {
            return _context.AlterarSenhaModel.Where(m => m.Id == model.TrocaId && m.Codigo == model.Codigo && m.Invalida == false).FirstOrDefault();

        }

        public ICollection<AlterarSenhaModel> GetAll(Guid vendedorId)
        {
            throw new NotImplementedException();
        }
    }
}
