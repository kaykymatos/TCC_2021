using FizzWare.NBuilder;
using System;
using TCCESTOQUE.Models;
using TCCESTOQUE.Models.Enum;

namespace Estoque.Test.Builder
{
    public class ProdutoModelBuilder : BuilderBase<ProdutoModel>
    {
        protected override void LoadDefault()
        {
            _builderInstance = Builder<ProdutoModel>.CreateNew()
                .With(x => x.Custo = Convert.ToDecimal(12.00))
                .With(x => x.Nome = "Cleberson")
                .With(x => x.Valor = Convert.ToDecimal(12.00))
                .With(x => x.Quantidade = 40)
                .With(x => x.UnidadeMedida = UnidadeDeMedidaEnum.Unidade);
        }
    }
}
