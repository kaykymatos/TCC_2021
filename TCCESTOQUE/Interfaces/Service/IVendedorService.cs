using FluentValidation.Results;
using System;
using TCCESTOQUE.Models;
using TCCESTOQUE.POCO;

namespace TCCESTOQUE.Interfaces.Service
{
    public interface IVendedorService : IBaseService<VendedorModel>
    {
        public ValidationResult PostCriacao(VendedorModel vendedorModel);

        public VendedorModel GetEdicao(Guid? id);

        public ValidationResult PutEdicao(Guid id, VendedorModel vendedorModel);

        public bool PostExclusao(Guid id);

        public object PostLogin(VendedorModel vendedorModel);

        public void AutenticarConta(Guid vendedorId);

        public bool EsqueciSenha(EmailClienteModel cliente);

        public AlterarSenhaModel GetOneAlterarSenha(Guid? trocaId);

        public ValidationResult AlterarSenha(AlterarSenha vendedorModel);
    }
}
