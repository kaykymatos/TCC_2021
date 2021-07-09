using System;
using TCCESTOQUE.Interfaces.Repository;
using TCCESTOQUE.Interfaces.Service;

namespace TCCESTOQUE.Service
{
    public class MovimentacaoService : IMovimentacaoService
    {
        private readonly IProdutoRepository _produtoRepo;
        public MovimentacaoService(IProdutoRepository produtoRepo)
        {
            _produtoRepo = produtoRepo;
        }

        public string BaixarEstoque(Guid produtoId, double quantidade, bool checar = true)
        {
            if (checar)
                ChecarEstoque(produtoId, quantidade);

            var produto = _produtoRepo.GetOne(produtoId);
            produto.Quantidade -= quantidade;

            _produtoRepo.PutEdicao(produto);
            return "";
        }

        public void SubirEstoque(Guid produtoId, double quantidade)
        {
            var produto = _produtoRepo.GetOne(produtoId);
            produto.Quantidade += quantidade;
            _produtoRepo.PutEdicao(produto);
        }

        public string ChecarEstoque(Guid produtoId, double quantidade)
        {
            var produto = _produtoRepo.GetOne(produtoId);
            if (produto.Quantidade - quantidade < 0)
                return $"Não há estoque suficiente para o seguinte produto solicitado : {produto.Nome}";
            return "";
        }

    }
}
