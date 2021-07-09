using FluentValidation;
using TCCESTOQUE.Models;
using TCCESTOQUE.Validacao.MensagensDeErro;

namespace TCCESTOQUE.Validacao.ValidacaoModels
{
    public class FornecedorValidador : AbstractValidator<FornecedorModel>
    {
        public FornecedorValidador()
        {
            RuleFor(f => f.NomeFantasia).NotEmpty().WithMessage(MensagensErroFornecedor.NomeFantasiaVazio)
                .MaximumLength(80).WithMessage(MensagensErroFornecedor.NomeFantasiaTamanhoMaximo)
                .MinimumLength(3).WithMessage(MensagensErroFornecedor.NomeFantasiaTamanhoMinimo);

            RuleFor(f => f.NomeFantasia).NotEmpty().WithMessage(MensagensDeErroPadrao.NomeVazio)
            .MaximumLength(80).WithMessage(MensagensDeErroPadrao.NomeTamanhoMaximo)
            .MinimumLength(3).WithMessage(MensagensDeErroPadrao.NomeTamanhoMinimo);

            RuleFor(f => f.Email).NotEmpty().WithMessage(MensagensDeErroPadrao.EmailVazio)
                .EmailAddress().WithMessage(MensagensDeErroPadrao.EmailFormatoInvalido)
                .MaximumLength(80).WithMessage(MensagensDeErroPadrao.EmailTamanhoMaximo)
                .MinimumLength(7).WithMessage(MensagensDeErroPadrao.EmailTamanhoMinimo);

            RuleFor(f => f.Telefone).NotEmpty().WithMessage(MensagensDeErroPadrao.TelefoneVazio)
                .Length(14).WithMessage(MensagensDeErroPadrao.TelefoneTamanho);

            RuleFor(f => f.Cnpj).NotEmpty().WithMessage(MensagensErroFornecedor.CnpjVazio)
                .Length(18).WithMessage(MensagensErroFornecedor.CnpjTamanho);

        }

    }
}
