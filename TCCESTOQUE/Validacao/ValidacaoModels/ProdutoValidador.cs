using FluentValidation;
using TCCESTOQUE.Models;
using TCCESTOQUE.Models.Enum;
using TCCESTOQUE.Validacao.MensagensDeErro;

namespace TCCESTOQUE.Validacao.ValidacaoModels
{
    public class ProdutoValidador : AbstractValidator<ProdutoModel>
    {
        public ProdutoValidador(bool editando = false)
        {

            RuleFor(p => p.Nome).NotEmpty().WithMessage(MensagensDeErroPadrao.NomeVazio)
                    .MaximumLength(80).WithMessage(MensagensDeErroPadrao.NomeTamanhoMaximo)
                    .MinimumLength(3).WithMessage(MensagensDeErroPadrao.NomeTamanhoMinimo);

            RuleFor(p => p.Descricao).MaximumLength(30).WithMessage(MensagensErroProduto.DescricaoTamanhoMaximo)
                .MinimumLength(3).WithMessage(MensagensErroProduto.DescricaoTamanhoMinimo);

            RuleFor(p => p.Custo).NotNull().WithMessage(MensagensErroProduto.CustoVazio)
                .NotEmpty().WithMessage(MensagensErroProduto.CustoVazio)
                .GreaterThan(0).WithMessage(MensagensErroProduto.CustoMinimo);

            RuleFor(p => p.Valor).NotNull().WithMessage(MensagensErroProduto.ValorUnitarioVazio)
                .NotEmpty().WithMessage(MensagensErroProduto.ValorUnitarioVazio)
                .GreaterThan(0).WithMessage(MensagensErroProduto.ValorUnitarioMinimo);

            RuleFor(p => p.UnidadeMedida).NotNull()
                .NotEqual(UnidadeDeMedidaEnum.Selecione).WithMessage(MensagensErroProduto.UnidadeDeMedidaInvalida);

            if (!editando)
                RuleFor(p => p.Quantidade).NotNull().WithMessage(MensagensErroProduto.QuantidadeVazia)
                    .NotEmpty().WithMessage(MensagensErroProduto.QuantidadeVazia)
                    .GreaterThan(0).WithMessage(MensagensErroProduto.QuantidadeMinima);
        }
    }
}
