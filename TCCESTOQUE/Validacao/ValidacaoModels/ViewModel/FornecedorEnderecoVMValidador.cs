using FluentValidation;
using TCCESTOQUE.Models.Enum;
using TCCESTOQUE.Validacao.MensagensDeErro;
using TCCESTOQUE.ViewModel;

namespace TCCESTOQUE.Validacao.ValidacaoModels.ViewModel
{
    public class FornecedorEnderecoVMValidador : AbstractValidator<FornecedorEnderecoViewModel>
    {
        public FornecedorEnderecoVMValidador()
        {
            RuleFor(f => f.NomeFantasia).NotEmpty().WithMessage(MensagensErroFornecedor.NomeFantasiaVazio)
                .MaximumLength(80).WithMessage(MensagensErroFornecedor.NomeFantasiaTamanhoMaximo)
                .MinimumLength(3).WithMessage(MensagensErroFornecedor.NomeFantasiaTamanhoMinimo);

            RuleFor(f => f.RazaoSocial).NotEmpty().WithMessage(MensagensErroFornecedor.RazaoSocialVazia)
               .MaximumLength(80).WithMessage(MensagensErroFornecedor.RazaoSocialTamanhoMaximo)
               .MinimumLength(3).WithMessage(MensagensErroFornecedor.RazaoSocialTamanhoMinimo);

            RuleFor(f => f.Email).NotEmpty().WithMessage(MensagensDeErroPadrao.EmailVazio)
                .EmailAddress().WithMessage(MensagensDeErroPadrao.EmailFormatoInvalido)
                .MaximumLength(80).WithMessage(MensagensDeErroPadrao.EmailTamanhoMaximo);

            RuleFor(f => f.Telefone).Length(14).WithMessage(MensagensDeErroPadrao.TelefoneTamanho);


            RuleFor(f => f.Cnpj).NotEmpty().WithMessage(MensagensErroFornecedor.CnpjVazio)
                .Length(18).WithMessage(MensagensErroFornecedor.CnpjTamanho);

            RuleFor(e => e.Cep).NotEmpty().WithMessage(MensagensDeErroEndereco.CepVazio)
                .Length(9).WithMessage(MensagensDeErroEndereco.CepTamanho);

            RuleFor(e => e.Logradouro).NotEmpty().WithMessage(MensagensDeErroEndereco.LogradouroVazio)
                .MaximumLength(80).WithMessage(MensagensDeErroEndereco.LogradouroTamanhoMaximo)
                .MinimumLength(3).WithMessage(MensagensDeErroEndereco.LogradouroTamanhoMinimo);

            RuleFor(e => e.Complemento).MaximumLength(80).WithMessage(MensagensDeErroEndereco.ComplementoTamanhoMaximo)
                .MinimumLength(3).WithMessage(MensagensDeErroEndereco.ComplementoTamanhoMinimo);

            RuleFor(e => e.Numero).NotNull().WithMessage(MensagensDeErroEndereco.NumeroVazio)
                .NotEmpty().WithMessage(MensagensDeErroEndereco.NumeroVazio);

            RuleFor(e => e.Bairro).NotEmpty().WithMessage(MensagensDeErroEndereco.BairroVazio)
                .MaximumLength(80).WithMessage(MensagensDeErroEndereco.BairroTamanhoMaximo)
                .MinimumLength(2).WithMessage(MensagensDeErroEndereco.BairroTamanhoMinimo);

            RuleFor(e => e.Localidade).NotEmpty().WithMessage(MensagensDeErroEndereco.LocalidadeVazio)
                .MaximumLength(80).WithMessage(MensagensDeErroEndereco.LocalidadeTamanhoMaximo)
                .MinimumLength(3).WithMessage(MensagensDeErroEndereco.LocalidadeTamanhoMinimo);

            RuleFor(v => v.Uf).NotEmpty().WithMessage(MensagensDeErroEndereco.UfVazio)
                .NotEqual(UnidadeFederalEnum.Null).WithMessage(MensagensDeErroEndereco.UfVazio);
        }
    }
}
