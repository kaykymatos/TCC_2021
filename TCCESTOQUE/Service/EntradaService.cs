using System;
using System.Collections.Generic;
using TCCESTOQUE.Interfaces.Repository;
using TCCESTOQUE.Interfaces.Service;
using TCCESTOQUE.Models;

namespace TCCESTOQUE.Service
{
    public class EntradaService : IEntradaService
    {
        private readonly IEntradaRepository _entradaRepo;
        private readonly IProdutoRepository _produtoRepo;
        private readonly IMovimentacaoService _movimentacaoService;
        public EntradaService(IEntradaRepository entradaRepo, IProdutoRepository produtoRepo, IMovimentacaoService movimentacao)
        {
            _entradaRepo = entradaRepo;
            _produtoRepo = produtoRepo;
            _movimentacaoService = movimentacao;
        }

        public string CancelarEntrada(EntradaModel entrada)
        {
            entrada = GetOne(entrada.EntradaId);
            entrada.Cancelada = true;
            var erro = _movimentacaoService.BaixarEstoque(entrada.ProdutoId, entrada.Quantidade);
            if (erro != "")
                return erro;
            _entradaRepo.PutEdicao(entrada);
            return "";
        }

        public ICollection<EntradaModel> GetAll(Guid id)
        {
            return _entradaRepo.GetAll(id);
        }

        public EntradaModel GetOne(Guid? id)
        {
            return _entradaRepo.GetOne(id);
        }

        public void PostEntrada(EntradaModel entrada)
        {
            var produto = _produtoRepo.GetOne(entrada.ProdutoId);
            entrada.Valor = produto.Valor;
            entrada.Custo = produto.Custo;
            _movimentacaoService.SubirEstoque(entrada.ProdutoId, entrada.Quantidade);
            _entradaRepo.PostCriacao(entrada);
        }
    }
}
