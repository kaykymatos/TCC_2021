namespace TCCESTOQUE.Validacao.MensagensDeErro
{
    public static class MensagensErroProduto
    {
        #region Valores Nulos
        public static string CustoVazio = "Insira o custo";
        public static string ValorUnitarioVazio = "Insira o valor unitario";
        public static string QuantidadeVazia = "Insira a quantidade";
        public static string FornecedorVazio = "O campo Fornecedor não pode ser nulo";
        #endregion

        #region Tamanho maximo        
        public static string DescricaoTamanhoMaximo = "O campo Descrição excedeu o máximo de caracteres ";
        #endregion

        #region Tamanho minimo
        public static string DescricaoTamanhoMinimo = "O campo Descrição tem que ter no mínimo 3 caracteres ";
        public static string DataDeEntradaFutura = "O campo Data não pode ser futura";
        public static string CustoMinimo = "O campo Custo tem que ter no mínimo 1";
        public static string ValorUnitarioMinimo = "O campo Valor Unitário tem que ter no mínimo 1";
        public static string QuantidadeMinima = "O campo Quantidade tem que ter no mínimo 1";
        #endregion

        #region Valores Não numéricos
        public static string UnidadeDeMedidaInvalida = "Informe a unidade de medida";
        #endregion
    }
}
