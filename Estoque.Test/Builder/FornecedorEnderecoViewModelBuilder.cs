using FizzWare.NBuilder;
using TCCESTOQUE.Models.Enum;
using TCCESTOQUE.ViewModel;

namespace Estoque.Test.Builder
{
    public class FornecedorEnderecoViewModelBuilder : BuilderBase<FornecedorEnderecoViewModel>
    {
        protected override void LoadDefault()
        {
            _builderInstance = Builder<FornecedorEnderecoViewModel>.CreateNew()
                .With(x => x.Telefone = "(11)99858-5826")
                .With(x => x.Cnpj = "01.222.333/0001-99")
                .With(x => x.Email = "mail@gmail.com")
                .With(x => x.NomeFantasia = "Nome do Fornecedor")
                .With(x => x.Bairro = "Bairro central")
                .With(x => x.Cep = "86945-857")
                .With(x => x.Complemento = "xxx complemento")
                .With(x => x.Localidade = "Cidade xxx")
                .With(x => x.Logradouro = "logradouro")
                .With(x => x.Numero = "1")
                .With(x => x.RazaoSocial = "Marcolino pereira")
                .With(x => x.Uf = UnidadeFederalEnum.SP);
        }
    }
}
