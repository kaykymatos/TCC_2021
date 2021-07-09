using FluentValidation;
using System;
using TCCESTOQUE.Models;
using TCCESTOQUE.Models.Enum;
using TCCESTOQUE.Validacao.MensagensDeErro;

namespace TCCESTOQUE.ValidadorVendedor
{
    public class VendedorValidador : AbstractValidator<VendedorModel>
    {
        public VendedorValidador()
        {
            RuleFor(v => v.Nome).NotEmpty().WithMessage(MensagensDeErroPadrao.NomeVazio)
                .MaximumLength(80).WithMessage(MensagensDeErroPadrao.NomeTamanhoMaximo)
                .MinimumLength(3).WithMessage(MensagensDeErroPadrao.NomeTamanhoMinimo);

            RuleFor(v => v.Email).NotEmpty().WithMessage(MensagensDeErroPadrao.EmailVazio)
                .EmailAddress().WithMessage(MensagensDeErroPadrao.EmailFormatoInvalido)
                .MaximumLength(100).WithMessage(MensagensDeErroPadrao.EmailTamanhoMaximo);

            RuleFor(v => v.Senha).NotEmpty().WithMessage(MensagensErroVendedor.SenhaVazia)
                .MaximumLength(50).WithMessage(MensagensErroVendedor.SenhaTamanhoMaximo)
                .MinimumLength(8).WithMessage(MensagensErroVendedor.SenhaTamanhoMinimo);

            RuleFor(v => v.DataNascimento).NotEmpty().WithMessage(MensagensErroVendedor.DataNascimentoVazia)
                .Must(IdadeMinima).WithMessage(MensagensErroVendedor.DataTamanhoMinimo);

            RuleFor(v => v.Telefone).Length(14).WithMessage(MensagensDeErroPadrao.TelefoneTamanho);

            RuleFor(v => v.Sexo).NotEqual(SexoEnum.Selecione).WithMessage(MensagensDeErroPadrao.SexoInvalido);
        }
        private static bool IdadeMinima(DateTime data)
        {
            return data <= DateTime.Today.AddYears(-18);
        }

    }
}
