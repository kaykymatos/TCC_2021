using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using TCCESTOQUE.Interfaces.Repository;
using TCCESTOQUE.Interfaces.Service;

namespace TCCESTOQUE.Service
{
    public class SelectListService : ISelectListService
    {
        private readonly IClienteRepository _clienteRepo;
        private readonly IProdutoRepository _produtoRepo;
        private readonly IFornecedorRepository _fornecedorRepo;
        private readonly IVendaRepository _vendaRepo;
        public SelectListService(IVendaRepository vendaRepo, IProdutoRepository produtoRepo, IClienteRepository clienteRepo,
                                 IFornecedorRepository fornecedorRepo)
        {
            _produtoRepo = produtoRepo;
            _vendaRepo = vendaRepo;
            _clienteRepo = clienteRepo;
            _fornecedorRepo = fornecedorRepo;
        }

        #region ListCliente
        public SelectList SelectListCliente(string dataValue, string textValue, Guid vendedorId)
        {
            return new SelectList(_clienteRepo.GetContext().Where(v => v.VendedorId == vendedorId && !v.Inativo), dataValue, textValue);
        }

        public SelectList SelectListCliente(string dataValue, string textValue, object selectedValueId, Guid vendedorId)
        {
            return new SelectList(_clienteRepo.GetContext().Where(v => v.VendedorId == vendedorId && !v.Inativo), dataValue, textValue, selectedValueId);
        }
        #endregion

        #region ListProduto
        public SelectList SelectListProduto(string dataValue, string textValue, Guid vendedorId)
        {
            return new SelectList(_produtoRepo.GetContext().Where(v => v.VendedorId == vendedorId && !v.Inativo), dataValue, textValue);
        }

        public SelectList SelectListProduto(string dataValue, string textValue, object selectedValueId, Guid vendedorId)
        {
            return new SelectList(_produtoRepo.GetContext().Where(v => v.VendedorId == vendedorId && !v.Inativo), dataValue, textValue, selectedValueId);
        }
        #endregion

        #region ListFornecedor
        public SelectList SelectListFornecedor(string dataValue, string textValue, Guid vendedorId)
        {
            return new SelectList(_fornecedorRepo.GetContext().Where(v => v.VendedorId == vendedorId && !v.Inativo), dataValue, textValue);
        }

        public SelectList SelectListFornecedor(string dataValue, string textValue, object selectedValueId, Guid vendedorId)
        {
            return new SelectList(_fornecedorRepo.GetContext().Where(v => v.VendedorId == vendedorId && !v.Inativo), dataValue, textValue, selectedValueId);
        }
        #endregion

        #region ListVenda
        public SelectList SelectListVenda(string dataValue, string textValue, Guid vendedorId)
        {
            return new SelectList(_vendaRepo.GetContext().Where(v => v.VendedorId == vendedorId), dataValue, textValue);
        }

        public SelectList SelectListVenda(string dataValue, string textValue, object selectedValueId, Guid vendedorId)
        {
            return new SelectList(_vendaRepo.GetContext().Where(v => v.VendedorId == vendedorId), dataValue, textValue, selectedValueId);
        }
        #endregion
    }
}
