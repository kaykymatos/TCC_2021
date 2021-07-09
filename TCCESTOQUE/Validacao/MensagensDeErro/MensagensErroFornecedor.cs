namespace TCCESTOQUE.Validacao.MensagensDeErro
{
    public static class MensagensErroFornecedor
    {
        #region Erros de Campos Nulos
        public static string RazaoSocialVazia = "Informe a Razão social";
        public static string NomeFantasiaVazio = "Informe o Nome fantasia";
        public static string ProdutoVazio = "Insira um produto";
        public static string CnpjVazio = "Insira o Cnpj";
        #endregion

        #region Erros ao exeder o tamanho maximo de um campo
        public static string NomeFantasiaTamanhoMaximo = "O campo Nome Fantasia excedeu o máximo de caracteres";
        public static string RazaoSocialTamanhoMaximo = "O campo Razão Social tem que ter no mínimo 3 caracteres";
        #endregion

        #region Já cadastrado
        public static string CnpjJaCadastrado = "O CNPJ já encontra-se cadastrado";
        public static string RazaoSocialJaCadastrada = "A Razão Social já encontra-se cadastrado";
        public static string NomeFantasiajaCadastrado = "O Nome Fantasia já encontra-se cadastrado";
        #endregion

        #region Erros ao não alcançar o mínimo de caracteres        
        public static string NomeFantasiaTamanhoMinimo = "O campo Nome Fantasia tem que ter no mínimo 3 caracteres";
        public static string RazaoSocialTamanhoMinimo = "O campo Razao Social tem que ter no mínimo 3 caracteres";
        #endregion

        #region Tamanho obrigatorio
        public static string CnpjTamanho = "O campo Cnpj deve ter 18 caracteres";
        #endregion

        #region Informar
        public static string InformeFornecedor = "Informe o fornecedor";
        #endregion
    }
}
