using System;
using TCCESTOQUE.Models;

namespace TCCESTOQUE.Interfaces.Service
{
    public interface IVendaItensService : IBaseService<VendaItensModel>
    {
        public string PostItem(VendaItensModel vendaItens);

        public string PutItemEdicao(VendaItensModel vendaItens);

        public bool PostItemExclusao(Guid vendaItensId);
    }
}
