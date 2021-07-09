using AutoMapper;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using TCCESTOQUE.Interfaces.Repository;
using TCCESTOQUE.Interfaces.Service;
using TCCESTOQUE.Models;
using TCCESTOQUE.Validacao.Formatacao;
using TCCESTOQUE.Validacao.ValidacaoModels;
using TCCESTOQUE.Validacao.ValidacaoModels.ViewModel;
using TCCESTOQUE.ViewModel;

namespace TCCESTOQUE.Service
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IEntradaRepository _entradaRepository;
        private readonly IMapper _mapper;
        public ProdutoService(IProdutoRepository produtoRepository, IEntradaRepository entradaRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _entradaRepository = entradaRepository;
            _mapper = mapper;
        }

        public ICollection<ProdutoModel> GetAll(Guid vendedorId)
        {
            return _produtoRepository.GetAll(vendedorId);
        }

        public ProdutoModel GetOne(Guid? id)
        {
            if (id == null)
                return null;

            return _produtoRepository.GetOne(id);
        }

        public ProdutoModel GetEdicao(Guid? id)
        {
            if (id == null)
                return null;

            return _produtoRepository.GetEdicao(id);
        }

        public ValidationResult PostCriacao(ProdutoViewModel produtoModel)
        {
            var validador = new ProdutoVMValidador().Validate(produtoModel);

            var produto = _mapper.Map<ProdutoViewModel, ProdutoModel>(produtoModel);
            var entrada = _mapper.Map<ProdutoViewModel, EntradaModel>(produtoModel);

            if (!validador.IsValid)
                return validador;

            produto = FormataValores.FormataProduto(produto);
            _produtoRepository.PostCriacao(produto);

            entrada.ProdutoId = produto.ProdutoId;
            _entradaRepository.PostCriacao(entrada);

            return validador;
        }

        public bool PostExclusao(Guid id)
        {
            var produto = _produtoRepository.GetOne(id);
            if (produto == null)
                return false;
            produto.Inativo = true;
            _produtoRepository.PutEdicao(produto);
            return true;
        }

        public ValidationResult PutEdicao(ProdutoModel produtoModel)
        {

            produtoModel.Nome.ToUpper().Trim();
            var validador = new ProdutoValidador(true).Validate(produtoModel);
            if (!validador.IsValid)
                return validador;
            produtoModel = FormataValores.FormataProduto(produtoModel);
            _produtoRepository.PutEdicao(ConvertProduto(produtoModel));
            return validador;
        }

        public ProdutoModel ConvertProduto(ProdutoModel produto)
        {
            var info = GetOne(produto.ProdutoId);
            info.Valor = produto.Valor;
            info.UnidadeMedida = produto.UnidadeMedida;
            info.Nome = produto.Nome;
            info.Descricao = produto.Descricao;
            info.Custo = produto.Custo;
            return info;
        }
    }
}
