using TCCESTOQUE.Models;
using TCCESTOQUE.Service;
using TCCESTOQUE.ViewModel;

namespace TCCESTOQUE.Validacao.Formatacao
{
    public class FormataValores
    {
        public static VendedorModel FormataValoresVendedor(VendedorModel vendedor)
        {
            vendedor.Nome = vendedor.Nome.ToUpper().Trim();
            vendedor.Telefone = vendedor.Telefone?.Trim();
            vendedor.Email = vendedor.Email.Trim();
            vendedor.Senha = SecurityService.Criptografar(vendedor.Senha);
            return vendedor;
        }
        public static ProdutoModel FormataProduto(ProdutoModel prod)
        {
            prod.Nome = prod.Nome.ToUpper().Trim();
            return prod;
        }
        public static ClienteViewModel FormataCliente(ClienteViewModel cli)
        {
            cli.Nome = cli.Nome.ToUpper().Trim();
            cli.Logradouro = cli.Logradouro?.ToUpper().Trim();
            cli.Bairro = cli.Bairro.ToUpper().Trim();
            cli.Localidade = cli.Localidade.ToUpper().Trim();
            return cli;
        }
        public static FornecedorEnderecoViewModel FormataValoresFornecedorView(FornecedorEnderecoViewModel fornecedor)
        {
            fornecedor.NomeFantasia = fornecedor.NomeFantasia.ToUpper().Trim();
            fornecedor.RazaoSocial = fornecedor.RazaoSocial.ToUpper().Trim();
            fornecedor.Logradouro = fornecedor.Logradouro.ToUpper().Trim();
            fornecedor.Localidade = fornecedor.Localidade.ToUpper().Trim();
            fornecedor.Complemento = fornecedor.Complemento?.ToUpper().Trim();
            fornecedor.Bairro = fornecedor.Bairro.ToUpper().Trim();
            return fornecedor;
        }
    }
}
