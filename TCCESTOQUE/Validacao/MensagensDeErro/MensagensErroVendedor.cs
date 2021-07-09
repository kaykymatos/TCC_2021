namespace TCCESTOQUE.Validacao.MensagensDeErro
{
    public static class MensagensErroVendedor
    {
        #region Erros de Campos Nulos
        public static string SenhaVazia = "Insira a senha";
        public static string DataNascimentoVazia = "Informe a data de nascimento";
        #endregion

        #region Erros ao exeder o tamanho maximo de um campo
        public static string SenhaTamanhoMaximo = "A Senha excedeu o máximo de caracteres";
        public static string DataNascimentoTamanhoMaximo = "A Data de Nascimento não pode ser futura";
        #endregion

        #region Campo de login incorreto
        public static string SenhaIncorreta = "Senha incorreta";
        #endregion

        #region Erros ao não alcançar o mínimo de caracteres        
        public static string SenhaTamanhoMinimo = "A Seha tem que ter no mínimo 8 caracteres";
        public static string DataTamanhoMinimo = "A idade minima é 18 anos";
        #endregion

        #region Conta Inativa
        public static string ContaInativa = "Conta inativa, verifique seu e-mail para ativar a conta";
        #endregion

        #region Erro codigo
        public static string CodigoInvalido = "Código inválido";
        public static string CodigoVazio = "Insira o código";
        #endregion
    }
}
