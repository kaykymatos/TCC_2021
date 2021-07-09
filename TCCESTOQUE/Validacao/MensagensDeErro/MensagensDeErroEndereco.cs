namespace TCCESTOQUE.Validacao.MensagensDeErro
{
    public static class MensagensDeErroEndereco
    {
        #region Campos vazios
        public static string CepVazio = "Insira o cep";
        public static string LogradouroVazio = "Insira o logradouro";
        public static string NumeroVazio = "Insira o numero da residencia";
        public static string BairroVazio = "Insira o bairro";
        public static string LocalidadeVazio = "Insira a localidade";
        public static string UfVazio = "Informe a unidade federal";
        #endregion


        #region tamanho máximo
        public static string CepTamanho = "O cep deve ter 9 caracteres";
        public static string LogradouroTamanhoMaximo = "O logradouro excedeu o maximo de caracteres";
        public static string ComplementoTamanhoMaximo = "O complemento excedeu o maximo de caracteres";
        public static string NumeroTamanhoMaximo = "O número excedeu o maximo de caracteres";
        public static string BairroTamanhoMaximo = "O bairro excedeu o maximo de caracteres";
        public static string LocalidadeTamanhoMaximo = "A localidade excedeu o maximo de caracteres";
        #endregion

        #region tamanho mínimo
        public static string LogradouroTamanhoMinimo = "O logradouro tem que ter no mínimo 3 caracteres";
        public static string ComplementoTamanhoMinimo = "O complemento tem que ter no mínimo 3 caracteres";
        public static string NumeroTamanhoMinimo = "O numero invalido";
        public static string BairroTamanhoMinimo = "O bairro tem que ter no mínimo 2 caracteres";
        public static string LocalidadeTamanhoMinimo = "A localidade tem que ter no mínimo 3 caracteres";
        #endregion
    }
}
