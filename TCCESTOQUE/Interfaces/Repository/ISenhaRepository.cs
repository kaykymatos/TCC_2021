using System;
using TCCESTOQUE.Models;
using TCCESTOQUE.POCO;

namespace TCCESTOQUE.Interfaces.Repository
{
    public interface ISenhaRepository : IBaseRepository<AlterarSenhaModel>
    {
        public AlterarSenhaModel GetOneByCodigo(AlterarSenha model);
        public bool ChegarUltimaTroca(Guid? vendedorId);
    }
}
