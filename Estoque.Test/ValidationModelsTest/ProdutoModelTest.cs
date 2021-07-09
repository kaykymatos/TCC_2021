using Estoque.Test.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using TCCESTOQUE.Validacao.MensagensDeErro;
using TCCESTOQUE.Validacao.ValidacaoModels;
using Xunit;

namespace Estoque.Test.ValidationModelsTest
{
    public class ProdutoModelTest
    {
        private readonly ProdutoModelBuilder _builder;
        private readonly ProdutoValidador _validator;

        public ProdutoModelTest()
        {
            var provider = new ServiceCollection().AddScoped<ProdutoValidador>().BuildServiceProvider();
            _builder = new ProdutoModelBuilder();
            _validator = provider.GetService<ProdutoValidador>();
        }

        [Fact(DisplayName = "Classe válida")]
        public async Task ClasseValida()
        {
            var instance = _builder.Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.True(validation.IsValid);
        }

        #region Nome
        [Theory(DisplayName = "Nome Tamanho mínimo")]
        [InlineData("GG")]
        [InlineData("jk")]
        [InlineData("ka")]
        [InlineData("je")]
        public async Task NomeTamanhoMinimo(string nome)
        {
            var instance = _builder.With(x => x.Nome = nome).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.False(validation.IsValid);
            Assert.Contains(validation.Errors, x => x.ErrorMessage.Contains(MensagensDeErroPadrao.NomeTamanhoMinimo));
        }

        [Theory(DisplayName = "Nome Tamanho mínimo")]
        [InlineData("teste de tamanho máximo do campo de nome do produto model que é de no máximo 80 caracteres")]
        [InlineData("jiuuuihhhfiuhuifhiuehfhiuherfh iuuhihhhuhiuihhihiuhiuhiu iuiuhiuhiuhhinmnmm m mm m m m m  ")]
        public async Task NomeTamanhoMaximo(string nome)
        {
            var instance = _builder.With(x => x.Nome = nome).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.False(validation.IsValid);
            Assert.Contains(validation.Errors, x => x.ErrorMessage.Contains(MensagensDeErroPadrao.NomeTamanhoMaximo));
        }

        [Fact(DisplayName = "Nome Tamanho mínimo")]
        public async Task NomeVazio()
        {
            var instance = _builder.With(x => x.Nome = "").Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.False(validation.IsValid);
            Assert.Contains(validation.Errors, x => x.ErrorMessage.Contains(MensagensDeErroPadrao.NomeVazio));
        }
        #endregion

        #region Custo
        [Theory(DisplayName = "Teste do custo mínimo do produto")]
        [InlineData(0.00)]
        [InlineData(-1.00)]
        [InlineData(-2.00)]
        public async Task CustoValorMinimo(decimal custo)
        {
            var instance = _builder.With(x => x.Custo = custo).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.False(validation.IsValid);
            Assert.Contains(validation.Errors, x => x.ErrorMessage.Contains(MensagensErroProduto.CustoMinimo));
        }

        [Theory(DisplayName = "Teste do custo válido do produto")]
        [InlineData(100)]
        [InlineData(2.33)]
        [InlineData(4.50)]
        public async Task CustoValido(decimal custo)
        {
            var instance = _builder.With(x => x.Custo = custo).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.True(validation.IsValid);
        }

        [Fact(DisplayName = "Teste do custo vazio")]
        public async Task CustoVazio()
        {
            var instance = _builder.With(x => x.Custo = Convert.ToDecimal(null)).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.False(validation.IsValid);
            Assert.Contains(validation.Errors, x => x.ErrorMessage.Contains(MensagensErroProduto.CustoVazio));
        }
        #endregion

        #region ValorUnitario
        [Theory(DisplayName = "Valor unitário válido")]
        [InlineData(1.00)]
        [InlineData(2.99)]
        [InlineData(3.66)]
        [InlineData(4.57)]
        public async Task ValorUnitarioValido(decimal valor)
        {
            var instance = _builder.With(x => x.Valor = valor).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.True(validation.IsValid);
        }

        [Theory(DisplayName = "Valor unitário válido")]
        [InlineData(-1.00)]
        [InlineData(-2.99)]
        [InlineData(-3.66)]
        [InlineData(-4.57)]
        public async Task ValorUnitarioMinimo(decimal valor)
        {
            var instance = _builder.With(x => x.Valor = valor).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.False(validation.IsValid);
            Assert.Contains(validation.Errors, x => x.ErrorMessage.Contains(MensagensErroProduto.ValorUnitarioMinimo));
        }

        [Fact(DisplayName = "Valor unitário vazio")]
        public async Task ValorUnitarioVazio()
        {
            var instance = _builder.With(x => x.Valor = Convert.ToDecimal(null)).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.False(validation.IsValid);
            Assert.Contains(validation.Errors, x => x.ErrorMessage.Contains(MensagensErroProduto.ValorUnitarioVazio));
        }
        #endregion

        #region Quantidade
        [Theory(DisplayName = "Quantidade mínima de produto")]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(-5)]
        public async Task QuantidadeMinimo(int quant)
        {
            var instance = _builder.With(x => x.Quantidade = quant).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.False(validation.IsValid);
            Assert.Contains(validation.Errors, x => x.ErrorMessage.Contains(MensagensErroProduto.QuantidadeMinima));
        }

        [Theory(DisplayName = "Quantidade mínima de produto")]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(200)]
        [InlineData(400)]
        public async Task QuantidadeValida(int quant)
        {
            var instance = _builder.With(x => x.Quantidade = quant).Build();
            var validation = await _validator.ValidateAsync(instance);
            Assert.True(validation.IsValid);
        }
        #endregion
    }
}
