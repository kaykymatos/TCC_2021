using FluentValidation;
using TCCESTOQUE.Interfaces.Repository;
using TCCESTOQUE.Validacao.MensagensDeErro;
using TCCESTOQUE.ViewModel;

namespace TCCESTOQUE.Validacao.ValidacaoBusiness.ViewModel
{
    public class FornecedorEnderecoVMBusinessValidador : AbstractValidator<FornecedorEnderecoViewModel>
    {
        public FornecedorEnderecoVMBusinessValidador(IFornecedorRepository fornecedorRepo, FornecedorEnderecoViewModel fornecedor)
        {
            When(fe => fornecedorRepo.GetByCnpj(fe.Cnpj, fe.VendedorId)?.FornecedorId != fe.FornecedorId, () =>
            {
                RuleFor(f => f.Cnpj).Must(cnpj => fornecedorRepo.GetByCnpj(cnpj, fornecedor.VendedorId) == null)
                .WithMessage(MensagensErroFornecedor.CnpjJaCadastrado);
            });

            When(fe => fornecedorRepo.GetByRazaoSocial(fe.RazaoSocial, fe.VendedorId)?.FornecedorId != fe.FornecedorId, () =>
            {
                RuleFor(f => f.RazaoSocial).Must(razaoSocial => fornecedorRepo.GetByRazaoSocial(razaoSocial, fornecedor.VendedorId) == null)
                .WithMessage(MensagensErroFornecedor.RazaoSocialJaCadastrada);
            });

            When(fe => fornecedorRepo.GetByNomeFantsia(fe.NomeFantasia, fe.VendedorId)?.FornecedorId != fe.FornecedorId, () =>
            {
                RuleFor(f => f.NomeFantasia).Must(nomeFantasia => fornecedorRepo.GetByNomeFantsia(nomeFantasia, fornecedor.VendedorId) == null)
               .WithMessage(MensagensErroFornecedor.NomeFantasiajaCadastrado);
            });

            When(fe => fornecedorRepo.GetByEmail(fe.Email, fe.VendedorId)?.FornecedorId != fe.FornecedorId, () =>
            {
                RuleFor(f => f.Email).Must(email => fornecedorRepo.GetByEmail(email, fornecedor.VendedorId) == null)
                .WithMessage(MensagensDeErroPadrao.EmailJaCadastrado);
            });
        }
    }
}
