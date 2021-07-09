using Estoque.Test.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using TCCESTOQUE.Models.Enum;
using TCCESTOQUE.Validacao.MensagensDeErro;
using TCCESTOQUE.Validacao.ValidacaoModels;
using Xunit;

namespace Estoque.Test.ValidationViewModelTests
{

    public class ClienteViewModelTests
    {
        private readonly ClienteViewModelBuilder _builder;
        private readonly ClienteValidador _validator;
        public ClienteViewModelTests()
        {
            var provider = new ServiceCollection().AddScoped<ClienteValidador>().BuildServiceProvider();
            _builder = new ClienteViewModelBuilder();
            _validator = provider.GetService<ClienteValidador>();
        }
        [Fact(DisplayName = "Classe válida")]
        public async Task ClasseValida()
        {
            var instance = _builder.Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.True(validation.IsValid);
        }

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

        [Theory(DisplayName = "Teste Bairros Tamanho máximo")]
        [InlineData("Teste de bairros que estão com mais caracteres que o suportedo no campo de bairro onde o máximo é 80")]
        [InlineData("ydyugduygu yuguygugiojlkjlkjl 2313312 dewdwdwdedewdwedwed ijioojboitjbojtiojbojbojijijo")]
        [InlineData("lshkfjrnbvnsouojforefe frfefefefrfref2434342 tgtrgtrgrggrgtgr5435535 345435345llgtrgtrlkmg")]
        public async Task BairrosTamanhoMaximo(string bairro)
        {
            var instance = _builder.With(x => x.Bairro = bairro).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.False(validation.IsValid);
            Assert.Contains(validation.Errors, x => x.ErrorMessage.Contains(MensagensDeErroEndereco.BairroTamanhoMaximo));
        }

        [Theory(DisplayName = "Teste Bairros válidos")]
        [InlineData("d")]
        [InlineData("f")]
        [InlineData("S")]
        public async Task BairrostamanhoMinimo(string bairro)
        {
            var instance = _builder.With(x => x.Bairro = bairro).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.False(validation.IsValid);
        }

        [Fact(DisplayName = "Bairro não pode ser vazio")]
        public async Task BairroVazio()
        {
            var instancia = _builder.With(x => x.Bairro = "").Build();
            var validation = await _validator.ValidateAsync(instancia);
            Assert.False(validation.IsValid);
            Assert.Contains(validation.Errors, x => x.ErrorMessage.Contains(MensagensDeErroEndereco.BairroVazio));
        }
        #endregion

        #region Cep
        [Theory(DisplayName = "Teste de Ceps válidos")]
        [InlineData("01001-000")]
        [InlineData("86945-857")]
        [InlineData("75865-231")]
        [InlineData("95743-954")]
        public async Task CepsValidos(string cep)
        {
            var instance = _builder.With(x => x.Cep = cep).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.True(validation.IsValid);
        }
        [Fact(DisplayName = "Cep não pode ser nulo")]
        public async Task CepVazio()
        {
            var instance = _builder.With(x => x.Cep = "").Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.False(validation.IsValid);
            Assert.Contains(validation.Errors, x => x.ErrorMessage.Contains(MensagensDeErroEndereco.CepVazio));
        }
        #endregion

        #region Complemento
        [Theory(DisplayName = "Complementos válidos")]
        [InlineData("mercadinho caboom")]
        [InlineData("Praça do Antonio")]
        [InlineData("Rotatoria")]
        public async Task ComplementosValidos(string complemento)
        {
            var instance = _builder.With(x => x.Complemento = complemento).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.True(validation.IsValid);
        }

        [Theory(DisplayName = "Complementos Tamanho máximo")]
        [InlineData("ydyugduygu yuguygugiojlkjlkjl 2313312 dewdwdwdedewdwedwed ijioojboitjbojtiojbojbojijijoddssasd")]
        [InlineData("teste do complemento maior que o suportado pelo campo do complemento que é de 80 djadhgjagjdgj")]
        [InlineData("kfhsgddffgtrhtyuiopçlkhjgbvfgtrdew12334565tgfderfghynvhffrktkjdrdfgthfvgtghyjukolkjrdufdkss")]
        public async Task ComplementosTamanhoMaximo(string complemento)
        {
            var instance = _builder.With(x => x.Complemento = complemento).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.False(validation.IsValid);
            Assert.Contains(validation.Errors, x => x.ErrorMessage.Contains(MensagensDeErroEndereco.ComplementoTamanhoMaximo));
        }
        #endregion

        #region Cpf
        [Theory(DisplayName = "Cpfs válidos")]
        [InlineData("123.456.789-09")]
        [InlineData("986.745.826-36")]
        [InlineData("759.298.673-87")]
        [InlineData("253.978.443-43")]
        public async Task CpfsValidos(string cpf)
        {
            var instance = _builder.With(x => x.Cpf = cpf).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.True(validation.IsValid);
        }

        [Theory(DisplayName = "Cpfs invalidos")]
        [InlineData("1.456.789-09")]
        [InlineData("986.5.826-36")]
        [InlineData("759.29673-87")]
        [InlineData("253.97.443-43")]
        public async Task CpfsInvalidos(string cpf)
        {
            var instance = _builder.With(x => x.Cpf = cpf).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.False(validation.IsValid);
            Assert.Contains(validation.Errors, x => x.ErrorMessage.Contains(MensagensDeErroPadrao.CpfTamanho));
        }
        #endregion

        #region Email
        [Fact(DisplayName = "O Email pode ser vazio")]
        public async Task EmailPodeSerVazio()
        {
            var instance = _builder.With(a => a.Email = "").Build();

            var validation = await _validator.ValidateAsync(instance);

            Assert.False(validation.IsValid);
        }

        [Theory(DisplayName = "Validar tipos diferentes de emails válidos")]
        [InlineData("a@a.com")]
        [InlineData("joao@maria.com")]
        [InlineData("jessica1232@gmail.com.br")]
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

        #region Localidade
        [Theory(DisplayName = "Teste de localidades válidas")]
        [InlineData("São Paulo")]
        [InlineData("Campinas")]
        [InlineData("Sei")]
        [InlineData("Estadio Santos Dumon")]
        public async Task LocalidadesValidas(string localidade)
        {
            var instance = _builder.With(x => x.Localidade = localidade).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.True(validation.IsValid);
        }

        [Theory(DisplayName = "Teste de localidades válidas")]
        [InlineData("hkfjdjdjdjdjdjfjghyhgtrfdewsvgfrdfgfrghjkloiuytrgbhkutre234567654324679knjhjjhjga")]
        [InlineData("hkfjdjdjdjdjdjfjghyhgtrfdewsvgfrdfgfrghjkloiuyswqsqwsqwswqsqs67654324679knjhjjhjgahuhdwueid")]
        public async Task LocalidadesTamanhoMaximo(string localidade)
        {
            var instance = _builder.With(x => x.Localidade = localidade).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.False(validation.IsValid);
            Assert.Contains(validation.Errors, x => x.ErrorMessage.Contains(MensagensDeErroEndereco.LocalidadeTamanhoMaximo));
        }

        [Theory(DisplayName = "Teste de localidades válidas")]
        [InlineData("q")]
        [InlineData("1")]
        [InlineData("u")]
        [InlineData("L")]
        [InlineData("P")]
        public async Task LocalidadesTamanhoMinimo(string localidade)
        {
            var instance = _builder.With(x => x.Localidade = localidade).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.False(validation.IsValid);
            Assert.Contains(validation.Errors, x => x.ErrorMessage.Contains(MensagensDeErroEndereco.LocalidadeTamanhoMinimo));
        }
        #endregion

        #region Logradouro
        [Theory(DisplayName = "Teste de logradouros válidos")]
        [InlineData("logradouro do cliente")]
        [InlineData("sei")]
        [InlineData("araraquara")]
        [InlineData("teste de logradouro")]
        public async Task LogradourosValidos(string logradouros)
        {
            var instance = _builder.With(x => x.Logradouro = logradouros).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.True(validation.IsValid);
        }

        [Theory(DisplayName = "Teste de logradouros tamanho máximo")]
        [InlineData("hkfjdjdjdjdjdjfjghyhgtrfdewsvgfrdfgfrghjkloiuytrgbhkutre234567654324679knjhjjhjga")]
        [InlineData("hkfjdjdjdjdjdjfjghyhgtrfdewsvgfrdfgfrghjkloiuyswqsqwsqwswqsqs67654324679knjhjjhjgahuhdwueid")]
        public async Task LogradouroTamanhoMaximo(string logradouro)
        {
            var instance = _builder.With(x => x.Logradouro = logradouro).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.False(validation.IsValid);
            Assert.Contains(validation.Errors, x => x.ErrorMessage.Contains(MensagensDeErroEndereco.LogradouroTamanhoMaximo));
        }

        [Fact(DisplayName = "Teste logradouro não pode ser vazio")]
        public async Task LogradouroVazio()
        {
            var instance = _builder.With(x => x.Logradouro = "").Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.Contains(validation.Errors, x => x.ErrorMessage.Contains(MensagensDeErroEndereco.LogradouroVazio));
        }
        #endregion

        #region Nome
        [Theory(DisplayName = "Teste nomes válidos")]
        [InlineData("Joaquin")]
        [InlineData("Eva")]
        [InlineData("Juliana")]
        [InlineData("Paulo Afonso")]
        public async Task NomesValidos(string nome)
        {
            var instance = _builder.With(x => x.Nome = nome).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.True(validation.IsValid);
        }

        [Theory(DisplayName = "Teste nomes Tamanho mínimo")]
        [InlineData("Jo")]
        [InlineData("na")]
        [InlineData("tw")]
        [InlineData("qw")]
        public async Task NomesTamanhoMinimo(string nome)
        {
            var instance = _builder.With(x => x.Nome = nome).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.False(validation.IsValid);
            Assert.Contains(validation.Errors, x => x.ErrorMessage.Contains(MensagensDeErroPadrao.NomeTamanhoMinimo));
        }

        [Theory(DisplayName = "Teste nomes tamanho máximo")]
        [InlineData("hkfjdjdjdjdjdjfjghyhgtrfdewsvgfrdfgfrghjkloiuytrgbhkutre234567654324679knjhjjhjga")]
        [InlineData("dwehuieihdwehdihdehidiufnrefrepfepofoeijrjfrejoiuytrgbhkutre234567654324679knjhjjhjgaqeqweq")]
        public async Task NomesTamanhoMaximo(string nome)
        {
            var instance = _builder.With(x => x.Nome = nome).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.False(validation.IsValid);
            Assert.Contains(validation.Errors, x => x.ErrorMessage.Contains(MensagensDeErroPadrao.NomeTamanhoMaximo));
        }

        [Fact(DisplayName = "Teste Nome não pode ser vazio")]
        public async Task NomeVazio()
        {
            var instance = _builder.With(x => x.Nome = "").Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.False(validation.IsValid);
            Assert.Contains(validation.Errors, x => x.ErrorMessage.Contains(MensagensDeErroPadrao.NomeVazio));
        }
        #endregion

        #region Número
        [Theory(DisplayName = "Números válidos")]
        [InlineData("1")]
        [InlineData("2")]
        [InlineData("3")]
        [InlineData("600")]
        [InlineData("1000")]
        public async Task Numeros(string numero)
        {
            var instance = _builder.With(x => x.Numero = numero).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.True(validation.IsValid);
        }

        [Theory(DisplayName = "Números Inválidos")]
        [InlineData("")]
        [InlineData(null)]
        public async Task NumerosInvalidos(string numero)
        {
            var instance = _builder.With(x => x.Numero = numero).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.False(validation.IsValid);
        }
        #endregion

        #region Telefone
        [Theory(DisplayName = "Telefones válidos")]
        [InlineData("(11)99443-2283")]
        [InlineData("(12)13324-5675")]
        [InlineData("(13)97425-8975")]
        [InlineData("(14)76846-2234")]
        public async Task TelefonesValidos(string telefone)
        {
            var instance = _builder.With(x => x.Telefone = telefone).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.True(validation.IsValid);
        }

        [Theory(DisplayName = "Telefones inválidos")]
        [InlineData("(11)99443-22")]
        [InlineData("(12)13325675")]
        [InlineData("(13425-8975")]
        [InlineData("(1476846-2234")]
        public async Task TelefonesInvalidos(string telefone)
        {
            var instance = _builder.With(x => x.Telefone = telefone).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.False(validation.IsValid);
            Assert.Contains(validation.Errors, x => x.ErrorMessage.Contains(MensagensDeErroPadrao.TelefoneTamanho));
        }
        [Fact(DisplayName = "Telefone vazio")]
        public async Task TelefoneVazio()
        {
            var instance = _builder.With(x => x.Telefone = "").Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.False(validation.IsValid);
            Assert.Contains(validation.Errors, x => x.ErrorMessage.Contains(MensagensDeErroPadrao.TelefoneVazio));
        }
        #endregion

        #region Uf
        [Theory(DisplayName = "Ufs válidas")]
        [InlineData(UnidadeFederalEnum.SP)]
        [InlineData(UnidadeFederalEnum.TO)]
        [InlineData(UnidadeFederalEnum.GO)]
        [InlineData(UnidadeFederalEnum.MG)]
        [InlineData(UnidadeFederalEnum.PR)]
        [InlineData(UnidadeFederalEnum.MT)]
        public async Task UfValidas(UnidadeFederalEnum uf)
        {
            var instance = _builder.With(x => x.Uf = uf).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.True(validation.IsValid);
        }

        [Fact(DisplayName = "Uf vazia")]
        public async Task UfVazia()
        {
            var instance = _builder.With(x => x.Uf = UnidadeFederalEnum.Null).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.False(validation.IsValid);
            Assert.Contains(validation.Errors, x => x.ErrorMessage.Contains(MensagensDeErroEndereco.UfVazio));
        }
        #endregion
    }
}
