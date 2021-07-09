using FizzWare.NBuilder;
using System;
using TCCESTOQUE.Models;
using TCCESTOQUE.Models.Enum;

namespace Estoque.Test.Builder
{
    public class VendedorModelBuilder : BuilderBase<VendedorModel>
    {
        protected override void LoadDefault()
        {
            _builderInstance = Builder<VendedorModel>.CreateNew()
                .With(x => x.DataNascimento = DateTime.Today.AddYears(-18))
                .With(x => x.Email = "teste@gmail.com")
                .With(x => x.Nome = "João carlos")
                .With(x => x.Senha = "12345678")
                .With(x => x.Telefone = "(11)22334-1129")
                .With(x => x.Sexo = SexoEnum.Masculino);
        }
    }
}
