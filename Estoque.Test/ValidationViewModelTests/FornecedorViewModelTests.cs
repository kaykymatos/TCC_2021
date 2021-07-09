using Estoque.Test.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using TCCESTOQUE.Models.Enum;
using TCCESTOQUE.Validacao.MensagensDeErro;
using TCCESTOQUE.Validacao.ValidacaoModels.ViewModel;
using Xunit;

namespace Estoque.Test.ValidationViewModelTests
{

    public class FornecedorViewModelTests
    {
        private readonly FornecedorEnderecoViewModelBuilder _builder;
        private readonly FornecedorEnderecoVMValidador _validator;

        public FornecedorViewModelTests()
        {
            var provider = new ServiceCollection().AddScoped<FornecedorEnderecoVMValidador>().BuildServiceProvider();

            _builder = new FornecedorEnderecoViewModelBuilder();
            _validator = provider.GetService<FornecedorEnderecoVMValidador>();
        }

        [Fact(DisplayName = "A classe deve ser válida")]
        public async Task ClasseValida()
        {
            var instance = _builder.Build();

            var validation = await _validator.ValidateAsync(instance);

            Assert.True(validation.IsValid);
        }

        #region Telefone
        [Theory(DisplayName = "Telefones válidos!")]
        [InlineData("(11)99332-4966")]
        [InlineData("(11)93232-4776")]
        [InlineData("(11)94332-4886")]
        [InlineData("(11)24322-8756")]
        [InlineData("(11)95448-4153")]
        [InlineData("(11)75943-3428")]
        public async Task TelefoneValido(string telefone)
        {
            var instance = _builder.With(a => a.Telefone = telefone).Build();
            var validation = await _validator.ValidateAsync(instance);

            Assert.True(validation.IsValid);
        }
        [Theory(DisplayName = "Telefones não válidos!")]
        [InlineData("(11)993-4966")]
        [InlineData("(11)9332-4776")]
        [InlineData("(11)94332-886")]
        [InlineData("(11)24322-856")]
        [InlineData("(1)95444153")]
        [InlineData("(1175943-3428")]
        [InlineData("")]
        public async Task TelefoneNaoValido(string telefone)
        {
            var instance = _builder.With(a => a.Telefone = telefone).Build();
            var validation = await _validator.ValidateAsync(instance);

            Assert.False(validation.IsValid);
        }
        #endregion

        #region cnpj
        [Theory(DisplayName = "Cnpj válidos")]
        [InlineData("01.222.343/0001-79")]
        [InlineData("01.223.123/0001-69")]
        [InlineData("01.252.983/0001-59")]
        [InlineData("01.262.653/0001-49")]
        public async Task CnpjValido(string cnpj)
        {
            var instance = _builder.With(a => a.Cnpj = cnpj).Build();

            var validation = await _validator.ValidateAsync(instance);

            Assert.True(validation.IsValid);
        }
        [Theory(DisplayName = "Cnpj válidos")]
        [InlineData("01.222.43/0001-79")]
        [InlineData("013.123/0001-69")]
        [InlineData(".252.983/0001-59")]
        [InlineData("01.262.65300-49")]
        public async Task CnpjNaoValido(string cnpj)
        {
            var instance = _builder.With(a => a.Cnpj = cnpj).Build();

            var validation = await _validator.ValidateAsync(instance);

            Assert.False(validation.IsValid);
        }
        #endregion

        #region Email
        [Theory(DisplayName = "Validar tipos diferentes de emails válidos")]
        [InlineData("a@a.com")]
        [InlineData("joao@maria.com")]
        [InlineData("marcio@nizzola.com.br")]
        public async Task EmailsValidos(string email)
        {
            var instance = _builder.With(a => a.Email = email).Build();

            var validation = await _validator.ValidateAsync(instance);

            Assert.True(validation.IsValid);
        }

        [Theory(DisplayName = "Validar emails incorretos")]
        [InlineData("aa.com")]
        [InlineData("@maria.com")]
        [InlineData("1")]
        public async Task EmailsInvalidos(string email)
        {
            var instance = _builder.With(a => a.Email = email).Build();

            var validation = await _validator.ValidateAsync(instance);

            Assert.False(validation.IsValid);
            Assert.Contains(validation.Errors, x => x.ErrorMessage.Contains(MensagensDeErroPadrao.EmailFormatoInvalido));
        }

        [Theory(DisplayName = "O email não pode exceder 80 caracteres")]
        [InlineData("012345678900123456789001234567890012345678900123456789001234567890012345678900123456789001234567890")]
        public async Task EmailExcedeuTamanho(string email)
        {
            var instance = _builder.With(a => a.Email = email).Build();

            var validation = await _validator.ValidateAsync(instance);

            Assert.False(validation.IsValid);
            Assert.Contains(validation.Errors, x => x.ErrorMessage.Contains(MensagensDeErroPadrao.EmailTamanhoMaximo));
        }
        #endregion

        #region Nome Fantasia

        [Theory(DisplayName = "Nomes Fantasia válidos")]
        [InlineData("Interprise")]
        [InlineData("PlayStore")]
        [InlineData("Nike")]
        [InlineData("Amazon")]
        public async Task NomeFantasiaValido(string nome)
        {
            var instance = _builder.With(x => x.NomeFantasia = nome).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.True(validation.IsValid);
        }

        [Theory(DisplayName = "Nomes Fantasia tamanho mínimo")]
        [InlineData("a")]
        [InlineData("b")]
        [InlineData("jf")]
        public async Task NomeFantasiaTamanhoMinimo(string nome)
        {
            var instance = _builder.With(x => x.NomeFantasia = nome).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.False(validation.IsValid);
            Assert.Contains(validation.Errors, x => x.ErrorMessage.Contains(MensagensErroFornecedor.NomeFantasiaTamanhoMinimo));
        }
        [Theory(DisplayName = "Nomes Fantasia tamanho máximo")]
        [InlineData("ddasdadasdasdasdasdadasdsafgghhggjhgjhjghjhgjhgbfdvvdfvdvdffvfdvfdvdfvdfvdfvdsadsaer")]
        [InlineData("kfjdgsgen ejcnejn eew ewcwe cwcwec ewcewwc wecwecwec wecwecwe cwecwecwcec wewerwerwerewrew")]
        [InlineData("teste do nome fantasia tamanho mínimoque o fornecedor pode colocar no input werwerwerewr")]
        public async Task NomeFantasiaTamanhoMaximo(string nome)
        {
            var instance = _builder.With(x => x.NomeFantasia = nome).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.False(validation.IsValid);
        }
        #endregion

        #region Bairro
        [Theory(DisplayName = "Teste Bairros válidos")]
        [InlineData("Bairro São João Doc Campos")]
        [InlineData("Bairro São Augustin")]
        [InlineData("Se")]
        public async Task BairrosVailidos(string bairro)
        {
            var instance = _builder.With(x => x.Bairro = bairro).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.True(validation.IsValid);
        }


        [Theory(DisplayName = "Bairro excedeu o valor máximo!")]
        [InlineData("asndhidhuidihduihiusfdsfdsfdsfsdfsdfsdfsdfhiuuhuihuihiuhihuihuihuuihuihuhiuhiuhiuhiuuhuknkjnkjn")]
        [InlineData("ighiebhreieiiurevreverkvbrevrevfsdfsdfsdfsddfwefwefefsdfsferveerarvarevevevevervrevevrevevfdvb")]
        [InlineData("hlsdjfsdfjfllsfjlsfksfkkjwfkjjnbvglfwefwfwefewwefwefwfewfwefwefwefwefwefogkdtjjsmdfsdfsfsdfscscs")]
        public async Task BairroTamanhoMaximo(string bairro)
        {
            var instance = _builder.With(x => x.Bairro = bairro).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.False(validation.IsValid);
            Assert.Contains(validation.Errors, x => x.ErrorMessage.Contains(MensagensDeErroEndereco.BairroTamanhoMaximo));
        }
        [Theory(DisplayName = "Bairro tamanho mínimo!")]
        [InlineData("g")]
        [InlineData("s")]
        [InlineData("1")]
        public async Task BairroTamanhoMinimoValido(string bairro)
        {
            var instance = _builder.With(x => x.Bairro = bairro).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.False(validation.IsValid);
            Assert.Contains(validation.Errors, x => x.ErrorMessage.Contains(MensagensDeErroEndereco.BairroTamanhoMinimo));
        }
        #endregion

        #region Cep
        [Theory(DisplayName = "Teste de Ceps válidos")]
        [InlineData("13455-678")]
        [InlineData("75862-231")]
        [InlineData("55455-678")]
        [InlineData("32895-678")]
        public async Task CepValido(string cep)
        {
            var instance = _builder.With(x => x.Cep = cep).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.True(validation.IsValid);
        }
        [Theory(DisplayName = "Teste de Ceps não válidos")]
        [InlineData("345-67")]
        [InlineData("134-678")]
        [InlineData("125-678")]
        [InlineData("12.3467")]
        [InlineData("12.35-68")]
        public async Task CepNaoValido(string cep)
        {
            var instance = _builder.With(x => x.Cep = cep).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.False(validation.IsValid);
            Assert.Contains(validation.Errors, x => x.ErrorMessage.Contains(MensagensDeErroEndereco.CepTamanho));
        }
        #endregion

        #region Localidade
        [Theory(DisplayName = "Teste tamanho mínimo localidade")]
        [InlineData("")]
        [InlineData("12")]
        [InlineData("h")]
        [InlineData("jd")]
        public async Task LocalidadeTamanhoMinimo(string localidade)
        {
            var instance = _builder.With(x => x.Localidade = localidade).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.False(validation.IsValid);
            Assert.Contains(validation.Errors, x => x.ErrorMessage.Contains(MensagensDeErroEndereco.LocalidadeTamanhoMinimo));
        }
        [Theory(DisplayName = "Teste tamanho máximo localidade")]
        [InlineData("hfhfhfgggdhdsasdsadasdasdsadaddwqdqwdwqdqwdwqdwqdwdwqdwqdwqdqdyubuuyyyuububbyuyubbbyubbuyybbub")]
        [InlineData("teste do tamanho máximo do campo de localidade que é de 80 caracteres no campo em que temos que escrever")]
        [InlineData("teste de localidade adequada para o tamanho proposto no validadeo xsnxkxkjnjkjnkncjkjnnknkjkndcsucbsdcbusdbc")]
        public async Task LocalidadeTamanhoMaximo(string localidade)
        {
            var instance = _builder.With(x => x.Localidade = localidade).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.False(validation.IsValid);
            Assert.Contains(validation.Errors, x => x.ErrorMessage.Contains(MensagensDeErroEndereco.LocalidadeTamanhoMaximo));
        }
        [Theory(DisplayName = "Teste localidades valida")]
        [InlineData("teste localidade válida")]
        [InlineData("bairro de la")]
        [InlineData("brasil")]
        public async Task LocalidadeValida(string localidade)
        {
            var instance = _builder.With(x => x.Localidade = localidade).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.True(validation.IsValid);
        }

        #endregion

        #region Logradouro
        [Theory(DisplayName = "Teste tamanho mínimo logradouro")]
        [InlineData("12")]
        [InlineData("h")]
        [InlineData("jd")]
        public async Task LogradouroTamanhoMinimo(string logradouro)
        {
            var instance = _builder.With(x => x.Logradouro = logradouro).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.False(validation.IsValid);
            Assert.Contains(validation.Errors, x => x.ErrorMessage.Contains(MensagensDeErroEndereco.LogradouroTamanhoMinimo));
        }
        [Theory(DisplayName = "Teste tamanho máximo logradouro")]
        [InlineData("jdjdjfjfhdgsgdgeftfdrtgyhjykdfghtcdcsdccscdscdscsdcdscyuhgfrdswertgfdfssdvdvvvfddsfff")]
        [InlineData("jdjdjfjfhdgsgdgeftfdrtgyhhkhkhkhkjjjjgjgfjfjfffffjfjjykdfghtyuhgfrdswertgsasdsafdfss")]
        [InlineData("jdjdjfjfhdgsgdgeftfdrtgyhjykdfghtyuhgfrdswertgfdfssdffgjgjfjgfjggfjkjkhkghfhhfhfhghsjfhdf")]
        public async Task LogradouroTamanhoMaximo(string logradouro)
        {
            var instance = _builder.With(x => x.Logradouro = logradouro).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.False(validation.IsValid);
        }
        [Theory(DisplayName = "Teste tamanho máximo localidade")]
        [InlineData("rua tal")]
        [InlineData("teste de localidade")]
        [InlineData("localidade válida")]
        public async Task LogradouroValido(string logradouro)
        {
            var instance = _builder.With(x => x.Logradouro = logradouro).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.True(validation.IsValid);
        }
        #endregion

        #region Complemento
        [Theory(DisplayName = "Teste de Complementos válidos")]
        [InlineData("sea")]
        [InlineData("padovani")]
        [InlineData("marechal")]
        [InlineData("mercado")]
        public async Task ComplementoValido(string complemento)
        {
            var instance = _builder.With(x => x.Complemento = complemento).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.True(validation.IsValid);
        }

        [Theory(DisplayName = "Teste de Complementos não válidos")]
        [InlineData("se")]
        [InlineData("1")]
        [InlineData("d")]
        [InlineData("32")]
        public async Task ComplementoTamanhoMinimo(string complemento)
        {
            var instance = _builder.With(x => x.Complemento = complemento).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.False(validation.IsValid);
        }
        [Theory(DisplayName = "Teste de Complementos tamanho máximo")]
        [InlineData("teste do complemento tamanho máximo do endereço do fornecedomodel que é de 80 caracteres no máximo")]
        [InlineData("jdjdjfjfhdgsgdgeftfdrtgyhjykdfghtyuhgfrdswertgfdfssbhjbehrebjffjhrebfhbfhjefjbrhfefcdscferfrfrefr")]
        [InlineData("iuhiuwciuwhiuchuihuicuiwechiewhiuceoccwlewldedlwdwjedlwdcsdcdscdscsdcsdcscscsdcdcscdcdscsdcdcsdcwe")]
        [InlineData("lgkfhhjfdjjfkdhiudwehiudewuhdhdiuwehiudewuidhuediedhweuhdiewdscsdcsdcsdcsdcdscdscsdcsdcscsdcscschddew")]
        public async Task ComplementoTamanhoMaximo(string complemento)
        {
            var instance = _builder.With(x => x.Complemento = complemento).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.False(validation.IsValid);
        }
        #endregion

        #region Numero
        [Theory(DisplayName = "teste de números válidos")]
        [InlineData("1")]
        [InlineData("2")]
        [InlineData("3")]
        [InlineData("500")]
        [InlineData("37")]
        public async Task NumerosValidos(string numero)
        {
            var instance = _builder.With(x => x.Numero = numero).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.True(validation.IsValid);
        }
        [Theory(DisplayName = "teste de números inválidos")]
        [InlineData("")]
        [InlineData(null)]
        public async Task NumerosInvalidos(string numero)
        {
            var instance = _builder.With(x => x.Numero = numero).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.False(validation.IsValid);
        }
        #endregion

        #region Razao Social
        [Theory(DisplayName = "teste de razões sociais válidas")]
        [InlineData("josé augusto")]
        [InlineData("Paulo fernandes")]
        [InlineData("Antônio rocheda")]
        [InlineData("Marcia algusta da silva")]
        public async Task RazaoSocialValida(string razao)
        {
            var instance = _builder.With(x => x.RazaoSocial = razao).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.True(validation.IsValid);
        }

        [Theory(DisplayName = "teste de razões sociais Tamanho máximo")]
        [InlineData("jdjdjfjfhdgsgdgeftfdrtgyhjykdfghtyuhgfrdswertgfdfsfewqkjnkjnsdjkfjdsfksdjfkjsjfsjdjfksd")]
        [InlineData("jdjssfdffffjfhdgsgdgeftfdrtgyhjykdfghtyuhgfrdswertgfdfsfewqkjnkjnsdjkfjdsfksdjfkjsjfsjdjfksd")]
        public async Task RazaoSocialTamanhoMaximo(string razao)
        {
            var instance = _builder.With(x => x.RazaoSocial = razao).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.False(validation.IsValid);
            Assert.Contains(validation.Errors, x => x.ErrorMessage.Contains(MensagensErroFornecedor.RazaoSocialTamanhoMaximo));
        }

        [Theory(DisplayName = "teste de razões sociais Tamanho mínimo")]
        [InlineData("te")]
        [InlineData("s")]
        [InlineData("1")]
        [InlineData("h")]
        [InlineData("ks")]
        public async Task RazaoSocialTamanhoMinimo(string razao)
        {
            var instance = _builder.With(x => x.RazaoSocial = razao).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.False(validation.IsValid);
            Assert.Contains(validation.Errors, x => x.ErrorMessage.Contains(MensagensErroFornecedor.RazaoSocialTamanhoMinimo));
        }
        #endregion

        #region Uf
        [Theory(DisplayName = "Teste UF válido")]
        [InlineData(UnidadeFederalEnum.BA)]
        [InlineData(UnidadeFederalEnum.SP)]
        [InlineData(UnidadeFederalEnum.MT)]
        public async Task UfValido(UnidadeFederalEnum uf)
        {
            var instance = _builder.With(x => x.Uf = uf).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.True(validation.IsValid);
        }

        [Fact(DisplayName = "Teste UF vazio")]

        public async Task UfVazio()
        {
            var instance = _builder.With(x => x.Uf = UnidadeFederalEnum.Null).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.False(validation.IsValid);
            Assert.Contains(validation.Errors, x => x.ErrorMessage.Contains(MensagensDeErroEndereco.UfVazio));
        }
        #endregion
    }
}
