using FluentValidation;
using System;
using TCCESTOQUE.Models.Enum;
using TCCESTOQUE.Validacao.MensagensDeErro;
using TCCESTOQUE.ViewModel;

namespace TCCESTOQUE.Validacao.ValidacaoModels.ViewModel
{
    public class ProdutoVMValidador : AbstractValidator<ProdutoViewModel>
    {
        public ProdutoVMValidador()
        {
            RuleFor(p => p.Nome).NotEmpty().WithMessage(MensagensDeErroPadrao.NomeVazio)
                    .MaximumLength(80).WithMessage(MensagensDeErroPadrao.NomeTamanhoMaximo)
                    .MinimumLength(3).WithMessage(MensagensDeErroPadrao.NomeTamanhoMinimo);

            RuleFor(p => p.Descricao).MaximumLength(30).WithMessage(MensagensErroProduto.DescricaoTamanhoMaximo)
                    .MinimumLength(3).WithMessage(MensagensErroProduto.DescricaoTamanhoMinimo);

            RuleFor(p => p.Custo).NotEmpty().WithMessage(MensagensErroProduto.CustoVazio)
                .NotNull().WithMessage(MensagensErroProduto.CustoVazio)
                    .GreaterThan(0).WithMessage(MensagensErroProduto.CustoMinimo);


            RuleFor(p => p.Valor).NotEmpty().WithMessage(MensagensErroProduto.ValorUnitarioVazio)
                .GreaterThan(0).WithMessage(MensagensErroProduto.ValorUnitarioMinimo);

            RuleFor(p => p.Quantidade).NotEmpty().WithMessage(MensagensErroProduto.QuantidadeVazia)
                .GreaterThan(0).WithMessage(MensagensErroProduto.QuantidadeMinima);

            RuleFor(p => p.UnidadeMedida).NotEqual(UnidadeDeMedidaEnum.Selecione).WithMessage(MensagensErroProduto.UnidadeDeMedidaInvalida);

            RuleFor(p => p.FornecedorId).NotEqual(Guid.Empty).WithMessage(MensagensErroFornecedor.InformeFornecedor);
        }
    }
}
