using FluentValidation.Results;
using System;
using System.Collections.Generic;
using TCCESTOQUE.EmailPages;
using TCCESTOQUE.Interfaces.Repository;
using TCCESTOQUE.Interfaces.Service;
using TCCESTOQUE.Models;
using TCCESTOQUE.POCO;
using TCCESTOQUE.Validacao.Formatacao;
using TCCESTOQUE.Validacao.ValidacaoBusiness;
using TCCESTOQUE.Validacao.ValidacaoModels;
using TCCESTOQUE.Validacao.ValidacaoPOCO;
using TCCESTOQUE.ValidadorVendedor;

namespace TCCESTOQUE.Service
{
    public class VendedorService : IVendedorService
    {
        private readonly IVendedorRepository _vendedorRepository;
        private readonly ICarrinhoRepository _carrinhoRepo;
        private readonly ISenhaRepository _alterarSenhaRepo;
        public VendedorService(IVendedorRepository vendedorRepository, ICarrinhoRepository carrinhoRepo, ISenhaRepository alterarSenhaRepo)
        {
            _vendedorRepository = vendedorRepository;
            _carrinhoRepo = carrinhoRepo;
            _alterarSenhaRepo = alterarSenhaRepo;
        }
        public ICollection<VendedorModel> GetAll(Guid vendedorId)
        {
            return _vendedorRepository.GetAll(vendedorId);
        }

        public VendedorModel GetOne(Guid? id)
        {
            if (id == null)
                return null;

            return _vendedorRepository.GetOne(id);
        }

        public AlterarSenhaModel GetOneAlterarSenha(Guid? trocaId)
        {
            return _alterarSenhaRepo.GetOne(trocaId);
        }

        public VendedorModel GetEdicao(Guid? id)
        {
            return _vendedorRepository.GetEdicao(id);
        }

        public ValidationResult PostCriacao(VendedorModel vendedorModel)
        {
            var validacao = ValidarVendedor(vendedorModel);
            if (validacao.IsValid)
            {
                vendedorModel = FormataValores.FormataValoresVendedor(vendedorModel);
                vendedorModel.Inativo = true;
                _vendedorRepository.PostCriacao(vendedorModel);
                EmailService.EnviarMensagem(new string[] { vendedorModel.Email }, null, SendEmail.EmailDeVerificacao(vendedorModel.VendedorId), "Autenticar conta", null);
                _carrinhoRepo.PostCriacao(new CarrinhoModel() { VendedorId = vendedorModel.VendedorId });
            }
            return validacao;
        }

        public ValidationResult PutEdicao(Guid id, VendedorModel vendedorModel)
        {
            var validacao = ValidarVendedor(vendedorModel);
            if (validacao.IsValid)
            {
                var vendedor = ConvertVendedor(vendedorModel);
                vendedor = FormataValores.FormataValoresVendedor(vendedor);
                _vendedorRepository.PutEdicao(vendedor);
            }
            return validacao;
        }

        public bool PostExclusao(Guid id)
        {
            var res = _vendedorRepository.GetOne(id);
            if (res == null)
                return false;

            res.Inativo = true;
            _vendedorRepository.PutEdicao(res);
            return true;
        }

        public object PostLogin(VendedorModel vendedorModel)
        {
            vendedorModel.Inativo = Convert.ToBoolean(_vendedorRepository.GetByEmail(vendedorModel.Email)?.Inativo);
            var validacao = new LoginValidador(_vendedorRepository, vendedorModel).Validate(vendedorModel);
            if (!validacao.IsValid)
                return validacao;

            var vendedor = _vendedorRepository.GetByEmail(vendedorModel.Email);
            return _vendedorRepository.PostLogin(vendedor);
        }

        private ValidationResult ValidarVendedor(VendedorModel vendedor)
        {
            var validacao = new VendedorValidador().Validate(vendedor);
            if (!validacao.IsValid)
                return validacao;

            var validacaoBusiness = new VendedorBusinessValidador(_vendedorRepository).Validate(vendedor);
            if (!validacaoBusiness.IsValid)
                return validacaoBusiness;

            return validacao;
        }

        private VendedorModel ConvertVendedor(VendedorModel vendedorModel)
        {
            var vendedor = _vendedorRepository.GetByEmail(vendedorModel.Email);
            vendedor.Email = vendedorModel.Email;
            vendedor.DataNascimento = vendedor.DataNascimento;
            vendedor.Nome = vendedorModel.Nome;
            vendedor.Senha = vendedorModel.Senha;
            vendedor.Telefone = vendedorModel.Telefone;
            return vendedor;
        }

        public bool EsqueciSenha(EmailClienteModel cliente)
        {
            if (cliente.Email != null || cliente.Email != "")
            {
                var cliId = _vendedorRepository.GetByEmail(cliente.Email)?.VendedorId;
                var disponivel = _alterarSenhaRepo.ChegarUltimaTroca(cliId);
                if (cliId != Guid.Empty && disponivel)
                {
                    var altSenha = new AlterarSenhaModel()
                    {
                        Codigo = new Random().Next(100000, 999999),
                        DataEmissão = DateTime.Now,
                        VendedorId = cliId
                    };
                    _alterarSenhaRepo.PostCriacao(altSenha);
                    EmailService.EnviarMensagem(new string[] { cliente.Email }, null,
                        SendEmail.EmailTrocaSenha(altSenha.VendedorId, altSenha.Id, altSenha.Codigo),
                        "Troca de senha", null);
                    return true;
                }
            }
            return false;
        }

        public void AutenticarConta(Guid vendedorId)
        {
            var vendedor = _vendedorRepository.GetById(vendedorId);
            vendedor.Inativo = false;
            _vendedorRepository.PutEdicao(vendedor);
        }

        public ValidationResult AlterarSenha(AlterarSenha senhaModel)
        {
            var alt = _alterarSenhaRepo.GetOneByCodigo(senhaModel);
            var validador = new AlterarSenhaValidador(alt).Validate(senhaModel);
            if (validador.IsValid)
            {
                var vend = _vendedorRepository.GetById(senhaModel.VendedorId);
                vend.Senha = SecurityService.Criptografar(senhaModel.NovaSenha);
                _vendedorRepository.PutEdicao(vend);
                alt.Invalida = true;
                _alterarSenhaRepo.PutEdicao(alt);
                return validador;
            }

            return validador;
        }
    }
}
