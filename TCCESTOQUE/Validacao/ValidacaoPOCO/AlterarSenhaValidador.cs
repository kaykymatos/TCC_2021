using FluentValidation;
using TCCESTOQUE.Models;
using TCCESTOQUE.POCO;
using TCCESTOQUE.Validacao.MensagensDeErro;

namespace TCCESTOQUE.Validacao.ValidacaoPOCO
{
    public class AlterarSenhaValidador : AbstractValidator<AlterarSenha>
    {
        public AlterarSenhaValidador(AlterarSenhaModel alt)
        {
            RuleFor(v => v.NovaSenha).NotEmpty().WithMessage(MensagensErroVendedor.SenhaVazia)
                .MaximumLength(50).WithMessage(MensagensErroVendedor.SenhaTamanhoMaximo)
                .MinimumLength(8).WithMessage(MensagensErroVendedor.SenhaTamanhoMinimo);

            RuleFor(v => v.Codigo).NotEmpty().WithMessage(MensagensErroVendedor.CodigoVazio)
                .LessThanOrEqualTo(999999).WithMessage(MensagensErroVendedor.CodigoInvalido)
                .GreaterThanOrEqualTo(100000).WithMessage(MensagensErroVendedor.CodigoInvalido)
                .Must(codigo => codigo == alt?.Codigo).WithMessage(MensagensErroVendedor.CodigoInvalido);
        }
    }
}
