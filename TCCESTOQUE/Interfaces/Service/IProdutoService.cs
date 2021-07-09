using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using TCCESTOQUE.Models;
using TCCESTOQUE.Models.Enum;
using TCCESTOQUE.ViewModel;

namespace TCCESTOQUE.Interfaces.Service
{
    public interface IProdutoService : IBaseService<ProdutoModel>
    {
        public ValidationResult PostCriacao(ProdutoViewModel produtoModel);

        public ValidationResult PutEdicao(ProdutoModel produtoModel);

        public ProdutoModel GetEdicao(Guid? id);

        public bool PostExclusao(Guid id);

        public SelectList SelectUnidadeDeMedida { get { return new SelectList(Enum.GetValues(typeof(UnidadeDeMedidaEnum))); } }
    }
}
