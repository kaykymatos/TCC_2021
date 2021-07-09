using TCCESTOQUE.Models;

namespace TCCESTOQUE.Interfaces.Repository
{
    public interface IFornecedorEnderecoRepository
    {
        public void PostCadastro(FornecedorEnderecoModel endereco);

        public void PutEdit(FornecedorEnderecoModel endereco);

        public FornecedorEnderecoModel GetEnderecoByFornecedorId(FornecedorModel fornecedor);
    }
}
