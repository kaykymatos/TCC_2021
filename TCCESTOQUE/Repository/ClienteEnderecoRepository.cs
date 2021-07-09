using System.Linq;
using TCCESTOQUE.Data;
using TCCESTOQUE.Interfaces.Repository;
using TCCESTOQUE.Models;

namespace TCCESTOQUE.Repository
{
    public class ClienteEnderecoRepository : IClienteEnderecoRepository
    {
        private readonly TCCESTOQUEContext _context;
        public ClienteEnderecoRepository(TCCESTOQUEContext context)
        {
            _context = context;
        }

        public void PostCriacao(ClienteEnderecoModel endereco)
        {
            _context.ClienteEnderecoModel.Add(endereco);
            _context.SaveChanges();
        }

        public void PutEdicao(ClienteEnderecoModel endereco)
        {
            _context.ClienteEnderecoModel.Update(endereco);
            _context.SaveChanges();
        }

        public ClienteEnderecoModel GetEnderecoByClienteId(ClienteModel cli)
        {
            return _context.ClienteEnderecoModel.Where(e => e.ClienteId == cli.ClienteId).FirstOrDefault();
        }
    }
}
