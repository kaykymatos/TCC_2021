using System;
using System.Collections.Generic;
using System.Linq;
using TCCESTOQUE.Interfaces.Repository;
using TCCESTOQUE.Interfaces.Service;
using TCCESTOQUE.Models;

namespace TCCESTOQUE.Service
{
    public class CarrinhoService : ICarrinhoService
    {
        private readonly ICarrinhoRepository _carrinhoRepo;
        private readonly IVendaRepository _vendaRepo;
        private readonly IMovimentacaoService _movimentacaoService;

        public CarrinhoService(ICarrinhoRepository carrinhoRepo, IVendaRepository vendaRepo, IMovimentacaoService movimentacaoService)
        {
            _carrinhoRepo = carrinhoRepo;
            _vendaRepo = vendaRepo;
            _movimentacaoService = movimentacaoService;
        }

        public ICollection<CarrinhoModel> GetAll(Guid vendedorId)
        {
            throw new NotImplementedException();
        }

        public ICollection<string> Finalizar(CarrinhoModel carrinho)
        {
            ICollection<string> erros = new List<string>();
            if (carrinho.ClienteId == Guid.Empty) { 
                erros.Add("Selecione o cliente");
                return erros;
            }
            var car = _carrinhoRepo.GetOneByVendedorId(carrinho.VendedorId);
            if (car.Itens.Any())
            {
                var venda = new VendaModel()
                {
                    ClienteId = carrinho.ClienteId,
                    DataVenda = DateTime.Now,
                    VendedorId = car.VendedorId
                };

                _vendaRepo.PostCriacao(venda);
                var itens = car.Itens.ToArray();
                var somaItens = itens.GroupBy(x => x.ProdutoId).Select(e => new { Id = e.Key, quantidade = e.Sum(x => x.Quantidade) }).ToArray();
                for (int i = 0; i < somaItens.Length; i++)
                {
                    var erro = _movimentacaoService.ChecarEstoque(somaItens[i].Id, somaItens[i].quantidade);
                    if (erro != "")
                        erros.Add(erro);
                }
                if (erros.Any())
                {
                    _vendaRepo.PostExclusao(venda);
                    return erros;
                }
                for (int i = 0; i < itens.Length; i++)
                {
                    if (itens[i].CarrinhoId != null)
                    {
                        itens[i].CarrinhoId = null;
                        itens[i].VendaId = venda.VendaId;
                        _movimentacaoService.BaixarEstoque(itens[i].ProdutoId, itens[i].Quantidade, false);
                    }
                }
                venda.Itens = itens;
                _vendaRepo.PutEdicao(venda);
            }
            else { erros.Add("Não existe nenhum item no carrinho"); }
            return erros;
        }

        public CarrinhoModel GetOne(Guid? id)
        {
            return _carrinhoRepo.GetOne(id);
        }
    }
}
