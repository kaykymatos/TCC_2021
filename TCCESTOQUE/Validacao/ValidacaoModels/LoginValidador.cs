using FluentValidation;
using TCCESTOQUE.Interfaces.Repository;
using TCCESTOQUE.Models;
using TCCESTOQUE.Service;
using TCCESTOQUE.Validacao.MensagensDeErro;

namespace TCCESTOQUE.Validacao.ValidacaoModels
{
    public class LoginValidador : AbstractValidator<VendedorModel>
    {
        public LoginValidador(IVendedorRepository vend, VendedorModel vendedor)
        {
            RuleFor(v => v).Must(ven => ven.Inativo == false).WithMessage(MensagensErroVendedor.ContaInativa);

            RuleFor(v => v.Email).Must(email => vend.GetByEmail(email) != null).WithMessage(MensagensDeErroPadrao.EmailNaoEncontrado);

            if (vendedor.Logar)
                RuleFor(v => v.Senha).Must(senha => vend.GetBySenha(senha, vendedor)?.Senha == vendedor.Senha).WithMessage(MensagensErroVendedor.SenhaIncorreta);
            else
                When(v => vend.GetByEmail(v.Email) != null, () =>
                {
                    RuleFor(v => v.Senha).Must(senha => vend.GetBySenha(senha, vendedor)?.Senha == SecurityService.Criptografar(vendedor.Senha)).WithMessage(MensagensErroVendedor.SenhaIncorreta);
                });

        }
    }
}
