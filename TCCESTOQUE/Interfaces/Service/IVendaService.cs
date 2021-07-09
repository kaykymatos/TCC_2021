using System;
using TCCESTOQUE.Models;

namespace TCCESTOQUE.Interfaces.Service
{
    public interface IVendaService : IBaseService<VendaModel>
    {
        public VendaModel GetEdicao(Guid? id);

        public object PutEdicao(Guid id, VendaModel venda);
        public bool Cancelar(Guid id);
    }
}
