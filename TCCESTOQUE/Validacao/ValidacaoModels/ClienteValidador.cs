using FluentValidation;
using TCCESTOQUE.Models.Enum;
using TCCESTOQUE.Validacao.MensagensDeErro;
using TCCESTOQUE.ViewModel;

namespace TCCESTOQUE.Validacao.ValidacaoModels
{
    public class ClienteValidador : AbstractValidator<ClienteViewModel>
    {
        public ClienteValidador()
        {
            RuleFor(v => v.Nome).NotEmpty().WithMessage(MensagensDeErroPadrao.NomeVazio)
               .MaximumLength(80).WithMessage(MensagensDeErroPadrao.NomeTamanhoMaximo)
               .MinimumLength(3).WithMessage(MensagensDeErroPadrao.NomeTamanhoMinimo);

            RuleFor(v => v.Telefone).NotEmpty().WithMessage(MensagensDeErroPadrao.TelefoneVazio)
                .Length(14).WithMessage(MensagensDeErroPadrao.TelefoneTamanho);

            RuleFor(v => v.Email).EmailAddress().WithMessage(MensagensDeErroPadrao.EmailFormatoInvalido)
               .MaximumLength(80).WithMessage(MensagensDeErroPadrao.EmailTamanhoMaximo);

            RuleFor(v => v.Cpf).Length(14).WithMessage(MensagensDeErroPadrao.CpfTamanho);

            RuleFor(v => v.Sexo).NotEqual(SexoEnum.Selecione).WithMessage(MensagensDeErroPadrao.SexoInvalido);

            #region endereco
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
            #endregion
        }
    }
}
