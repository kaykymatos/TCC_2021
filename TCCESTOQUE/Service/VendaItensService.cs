using System;
using System.Collections.Generic;
using TCCESTOQUE.Interfaces.Repository;
using TCCESTOQUE.Interfaces.Service;
using TCCESTOQUE.Models;

namespace TCCESTOQUE.Service
{
    public class VendaItensService : IVendaItensService
    {
        private readonly IVendaItensRepository _vendaItensRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMovimentacaoService _movimentacaoService;
        public VendaItensService(IVendaItensRepository vendaItensRepository, IMovimentacaoService movimentacaoService, IProdutoRepository produtoRepository)
        {
            _vendaItensRepository = vendaItensRepository;
            _movimentacaoService = movimentacaoService;
            _produtoRepository = produtoRepository;
        }

        public ICollection<VendaItensModel> GetAll(Guid vendedorId)
        {
            throw new NotImplementedException();
        }

        public VendaItensModel GetOne(Guid? id)
        {
            return _vendaItensRepository.GetOne(id);
        }

        public string PostItem(VendaItensModel vendaItens)
        {
            var res = _movimentacaoService.ChecarEstoque(vendaItens.ProdutoId, vendaItens.Quantidade);
            var produto = _produtoRepository.GetOne(vendaItens.ProdutoId);
            vendaItens.PrecoProduto = produto.Valor;
            vendaItens.CustoProduto = produto.Custo;
            if (res != "")
                return res;

            _vendaItensRepository.PostCriacao(vendaItens);
            return res;
        }

        public string PutItemEdicao(VendaItensModel vendaItens)
        {
            var res = _movimentacaoService.ChecarEstoque(vendaItens.ProdutoId, vendaItens.Quantidade);
            var produto = _produtoRepository.GetOne(vendaItens.ProdutoId);
            vendaItens.PrecoProduto = produto.Valor;
            vendaItens.CustoProduto = produto.Custo;
            if (res != "")
                return res;

            _vendaItensRepository.PutEdicao(vendaItens);
            return res;
        }

        public bool PostItemExclusao(Guid id)
        {
            var model = _vendaItensRepository.GetOne(id);
            _vendaItensRepository.PostExclusao(model);
            return true;
        }
    }
}
