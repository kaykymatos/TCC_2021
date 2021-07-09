using Estoque.Test.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using TCCESTOQUE.Models.Enum;
using TCCESTOQUE.Validacao.MensagensDeErro;
using TCCESTOQUE.ValidadorVendedor;
using Xunit;

namespace Estoque.Test.ValidationModelTests
{
    public class VendedorModelTest
    {
        private readonly VendedorModelBuilder _builder;
        private readonly VendedorValidador _validator;
        public VendedorModelTest()
        {
            var provider = new ServiceCollection().AddScoped<VendedorValidador>().BuildServiceProvider();

            _builder = new VendedorModelBuilder();
            _validator = provider.GetService<VendedorValidador>();
        }

        [Fact(DisplayName = "A classe deve ser válida")]
        public async Task ClasseValida()
        {
            var instance = _builder.Build();

            var validation = await _validator.ValidateAsync(instance);

            Assert.True(validation.IsValid);
        }

        #region Data de nascimento
        [Theory(DisplayName = "Teste de datas de nascimento válidas")]
        [InlineData("10/01/2000")]
        [InlineData("10/01/2003")]
        [InlineData("10/01/1999")]
        [InlineData("10/01/1899")]
        public async Task DataDeNascimentoValidas(string data)
        {
            var instance = _builder.With(x => x.DataNascimento = DateTime.Parse(data)).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.True(validation.IsValid);
        }
        [Theory(DisplayName = "Teste de datas de nascimento menor que 18 anos")]
        [InlineData("01/01/2005")]
        [InlineData("01/01/2006")]
        [InlineData("01/01/2010")]
        [InlineData("01/01/2011")]
        public async Task DataDeNascimentoMenor18(string data)
        {
            var instance = _builder.With(x => x.DataNascimento = DateTime.Parse(data)).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.False(validation.IsValid);
            Assert.Contains(validation.Errors, x => x.ErrorMessage.Contains(MensagensErroVendedor.DataTamanhoMinimo));
        }
        [Theory(DisplayName = "Teste de datas de nascimento futuras")]
        [InlineData("10/01/2500")]
        [InlineData("10/01/2070")]
        [InlineData("10/01/2077")]
        [InlineData("10/01/9999")]
        public async Task DataDeNascimentoFuturas(string dataStr)
        {
            var instance = _builder.With(x => x.DataNascimento = DateTime.Parse(dataStr)).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.False(validation.IsValid);
        }
        #endregion

        #region email
        [Theory(DisplayName = "Email deve ser válido!")]
        [InlineData("teste@gmail.com")]
        [InlineData("gmail@gmail.com")]
        [InlineData("marcos@gmail.com")]
        [InlineData("jessica@gmail.com")]
        public async Task EmailsValidos(string email)
        {
            var instance = _builder.With(x => x.Email = email).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.True(validation.IsValid);
        }

        [Fact(DisplayName = "Email nulo!")]
        public async Task EmailsNulo()
        {
            var instance = _builder.With(x => x.Email = "").Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.False(validation.IsValid);
            Assert.Contains(validation.Errors, x => x.ErrorMessage.Contains(MensagensDeErroPadrao.EmailVazio));
        }

        [Theory(DisplayName = "Teste de emails formato inválidos")]
        [InlineData("aaaaa")]
        [InlineData("aaaaa@")]
        [InlineData("@aaaaa")]
        [InlineData("aaaaa.com")]
        public async Task EmailFormatoInvalido(string email)
        {
            var instance = _builder.With(x => x.Email = email).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.False(validation.IsValid);
            Assert.Contains(validation.Errors, x => x.ErrorMessage.Contains(MensagensDeErroPadrao.EmailFormatoInvalido));
        }
        #endregion

        #region Nome
        [Theory(DisplayName = "Nome deve ser válido")]
        [InlineData("João Algusto Pereira da Silva")]
        [InlineData("Ariane joana dos campos")]
        [InlineData("Rosa Maria Valentina")]
        [InlineData("Eva")]
        public async Task NomesValidos(string nome)
        {
            var instance = _builder.With(x => x.Nome = nome).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.True(validation.IsValid);
        }
        [Theory(DisplayName = "Nome tamanho máximo")]
        [InlineData("Teste do tamanho do nome máximo de vendedor que é de 80 caracteres para preencher o input apenas")]
        [InlineData("teste iweifewhfwf fwefewfwefwfwef tgrrgtrgtr keknrtkjgnkjrtngjrt gkjrnkrngknrtng wefknknjewfnk")]
        [InlineData("jdhdgsdsdfsfs fsfsfwew ejoalasda sadadas d weff ffer frfrefrefefrfewqeweqwwewqeqehhh")]
        public async Task NomesTamanhoMaximo(string nome)
        {
            var instance = _builder.With(x => x.Nome = nome).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.False(validation.IsValid);
            Assert.Contains(validation.Errors, x => x.ErrorMessage.Contains(MensagensDeErroPadrao.NomeTamanhoMaximo));
        }

        [Theory(DisplayName = "Nome tamanho mínimo")]
        [InlineData("m")]
        [InlineData("ad")]
        [InlineData("ts")]
        public async Task NomesTamanhoMinimo(string nome)
        {
            var instance = _builder.With(x => x.Nome = nome).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.False(validation.IsValid);
            Assert.Contains(validation.Errors, x => x.ErrorMessage.Contains(MensagensDeErroPadrao.NomeTamanhoMinimo));
        }

        [Fact(DisplayName = "Nome nulo")]
        public async Task NomeNulo()
        {
            var instance = _builder.With(x => x.Nome = "").Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.False(validation.IsValid);
            Assert.Contains(validation.Errors, x => x.ErrorMessage.Contains(MensagensDeErroPadrao.NomeVazio));
        }
        #endregion

        #region Senha
        [Theory(DisplayName = "Senhas válidas")]
        [InlineData("12345678")]
        [InlineData("65853443")]
        [InlineData("iuuiught")]
        [InlineData("yugUGyugugguyggu")]
        public async Task SenhasValidas(string senha)
        {
            var instance = _builder.With(x => x.Senha = senha).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.True(validation.IsValid);
        }

        [Theory(DisplayName = "Senhas inválidas")]
        [InlineData("jf")]
        [InlineData("hda")]
        [InlineData("hjgdfsd")]
        [InlineData("1234567")]
        public async Task SenhasTamahoMinimo(string senha)
        {
            var instance = _builder.With(x => x.Senha = senha).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.False(validation.IsValid);
            Assert.Contains(validation.Errors, x => x.ErrorMessage.Contains(MensagensErroVendedor.SenhaTamanhoMinimo));
        }
        [Theory(DisplayName = "Senhas inválidas")]
        [InlineData("ddhgloirbcmfkgdhslfjsldjdlsnfue344243242mknkjhuhius")]
        [InlineData("ug113398791878931793173933dhkdhakdakedjejdkahkdaehdkjebd")]
        [InlineData("dugudgudydgyudgqdgwqduidwdqdhi8398219331321313jbjbjbhbbjbsjhs")]
        [InlineData("jdgewfugurgfyuefhegufehfuygufuuegfugyuyuyu67t6t67gbjhbhjjjbjb")]
        public async Task SenhasTamahoMaximo(string senha)
        {
            var instance = _builder.With(x => x.Senha = senha).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.False(validation.IsValid);
            Assert.Contains(validation.Errors, x => x.ErrorMessage.Contains(MensagensErroVendedor.SenhaTamanhoMaximo));
        }

        [Fact(DisplayName = "Senhas inválidas")]

        public async Task SenhasNula()
        {
            var instance = _builder.With(x => x.Senha = "").Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.False(validation.IsValid);
            Assert.Contains(validation.Errors, x => x.ErrorMessage.Contains(MensagensErroVendedor.SenhaVazia));
        }
        #endregion

        #region Telefone
        [Theory(DisplayName = "Telefones válidos")]
        [InlineData("(11)99332-7745")]
        [InlineData("(12)99455-7362")]
        [InlineData("(13)97682-6453")]
        [InlineData("(14)17292-7464")]
        [InlineData("(15)91238-9271")]
        public async Task TelefonesValidos(string telefone)
        {
            var instancia = _builder.With(x => x.Telefone = telefone).Build();
            var validation = await _validator.ValidateAsync(instancia);
            Assert.True(validation.IsValid);
        }

        [Theory(DisplayName = "Telefones inválidos")]
        [InlineData("1)99332-7745")]
        [InlineData("(129455-7362")]
        [InlineData("(1397682-6453")]
        [InlineData("(1172927464")]
        [InlineData("(15)91239271")]
        public async Task TelefonesInvalidos(string telefone)
        {
            var instancia = _builder.With(x => x.Telefone = telefone).Build();
            var validation = await _validator.ValidateAsync(instancia);
            Assert.False(validation.IsValid);
            Assert.Contains(validation.Errors, x => x.ErrorMessage.Contains(MensagensDeErroPadrao.TelefoneTamanho));
        }
        #endregion

        #region Sexo/Gênero
        [Fact(DisplayName = "Sexo válido")]
        public async Task SexoValido()
        {
            var instance = _builder.With(x => x.Sexo = SexoEnum.Masculino).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.True(validation.IsValid);
        }

        [Theory(DisplayName = "Generos válidos")]
        [InlineData(SexoEnum.Feminino)]
        [InlineData(SexoEnum.Masculino)]
        [InlineData(SexoEnum.Outros)]
        public async Task GenerosValidos(SexoEnum sexo)
        {
            var instance = _builder.With(x => x.Sexo = sexo).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.True(validation.IsValid);
        }
        #endregion
    }
}
