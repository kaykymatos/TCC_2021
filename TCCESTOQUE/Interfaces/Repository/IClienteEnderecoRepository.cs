using TCCESTOQUE.Models;

namespace TCCESTOQUE.Interfaces.Repository
{
    public interface IClienteEnderecoRepository
    {
        public void PostCriacao(ClienteEnderecoModel endereco);

        public void PutEdicao(ClienteEnderecoModel endereco);

        public ClienteEnderecoModel GetEnderecoByClienteId(ClienteModel cliente);
    }
}
