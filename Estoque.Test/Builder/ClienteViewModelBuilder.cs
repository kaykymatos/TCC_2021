using FizzWare.NBuilder;
using TCCESTOQUE.Models.Enum;
using TCCESTOQUE.ViewModel;

namespace Estoque.Test.Builder
{
    public class ClienteViewModelBuilder : BuilderBase<ClienteViewModel>
    {
        protected override void LoadDefault()
        {
            _builderInstance = Builder<ClienteViewModel>.CreateNew()
                 .With(x => x.Bairro = "Se")
                 .With(x => x.Cep = "67750-000")
                 .With(x => x.Complemento = "Praça Santa")
                 .With(x => x.Cpf = "123.456.789-09")
                 .With(x => x.Email = "newcode@gmail.com")
                 .With(x => x.Localidade = "São Paulo Itu")
                 .With(x => x.Logradouro = "logradouro do cliente")
                 .With(x => x.Nome = "Ana Catarina da Silva")
                 .With(x => x.Numero = "59")
                 .With(x => x.Telefone = "(11)99443-1123")
                 .With(x => x.Sexo = SexoEnum.Masculino)
                 .With(x => x.Uf = UnidadeFederalEnum.SP);
        }
    }
}
