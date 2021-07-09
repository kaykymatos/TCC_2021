using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TCCESTOQUE.Interfaces.Repository;
using TCCESTOQUE.Interfaces.Service;
using TCCESTOQUE.Models;

namespace TCCESTOQUE.Service
{
    public class VendaService : IVendaService
    {
        private readonly IVendaRepository _vendaRepository;
        private readonly IVendaItensRepository _vendaItensRepository;
        private readonly IMovimentacaoService _movimentacaoService;
        private readonly IMapper _mapper;

        public VendaService(IVendaRepository vendaRepository, IVendaItensRepository vendaItensRepository, IMovimentacaoService movimentacaoService, IMapper mapper)
        {
            _vendaRepository = vendaRepository;
            _vendaItensRepository = vendaItensRepository;
            _movimentacaoService = movimentacaoService;
            _mapper = mapper;
        }

        public ICollection<VendaModel> GetAll(Guid vendedorId)
        {
            return _vendaRepository.GetAll(vendedorId);
        }

        public VendaModel GetOne(Guid? id)
        {
            return _vendaRepository.GetOne(id);
        }

        public VendaModel GetEdicao(Guid? id)
        {
            return _vendaRepository.GetEdicao(id);
        }

        public object PutEdicao(Guid id, VendaModel venda)
        {
            venda.VendaId = id;
            _vendaRepository.PutEdicao(venda);
            return true;
        }

        public bool Cancelar(Guid id)
        {
            var venda = _vendaRepository.GetOne(id);
            if (venda.Itens.Any())
            {
                var itens = venda.Itens.ToArray();
                for (int i = 0; i < itens.Length; i++)
                {
                    if (itens[i].VendaId != null)
                    {
                        _movimentacaoService.SubirEstoque(itens[i].ProdutoId, itens[i].Quantidade);
                    }
                }
            }
            venda.Cancelada = true;
            _vendaRepository.PutEdicao(venda);
            return true;
        }
    }
}
