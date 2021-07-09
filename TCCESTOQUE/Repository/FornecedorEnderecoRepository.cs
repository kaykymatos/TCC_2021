using System.Linq;
using TCCESTOQUE.Data;
using TCCESTOQUE.Interfaces.Repository;
using TCCESTOQUE.Models;

namespace TCCESTOQUE.Repository
{
    public class FornecedorEnderecoRepository : IFornecedorEnderecoRepository
    {
        private readonly TCCESTOQUEContext _context;

        public FornecedorEnderecoRepository(TCCESTOQUEContext context)
        {
            _context = context;
        }

        public FornecedorEnderecoModel GetEnderecoByFornecedorId(FornecedorModel fornecedor)
        {
            return _context.FornecedorEnderecoModel.Where(e => e.FornecedorId == fornecedor.FornecedorId)?.FirstOrDefault();
        }

        public void PostCadastro(FornecedorEnderecoModel endereco)
        {
            _context.FornecedorEnderecoModel.Add(endereco);
            _context.SaveChanges();
        }

        public void PutEdit(FornecedorEnderecoModel endereco)
        {
            _context.FornecedorEnderecoModel.Update(endereco);
            _context.SaveChanges();
        }
    }
}
