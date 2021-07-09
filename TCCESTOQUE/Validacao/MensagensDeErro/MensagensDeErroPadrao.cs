namespace TCCESTOQUE.Validacao.MensagensDeErro
{
    public class MensagensDeErroPadrao
    {
        #region campos nulos
        public static string NomeVazio = "Insira o nome";
        public static string EmailVazio = "Insira o email";
        public static string TelefoneVazio = "Insira o telefone";
        public static string CpfVazio = "Insira o cpf";
        #endregion

        #region Tamanho maximo de tamanho
        public static string EmailTamanhoMaximo = "O email excedeu o máximo de caracteres";
        public static string NomeTamanhoMaximo = "O campo Nome excedeu o máximo de caracteres";

        #endregion

        #region Tamanho minimo
        public static string NomeTamanhoMinimo = "O Nome tem que ter no mínimo 3 caracteres";
        public static string EmailTamanhoMinimo = "O Email tem que ter no mínimo 7 caracteres";

        #endregion

        #region Tamanho obrigatorio
        public static string TelefoneTamanho = "O telefone deve ter 14 caracteres";
        public static string CpfTamanho = "O cpf deve ter 14 caracteres";

        #endregion

        #region Campo já cadastrado
        public static string TelefoneJaCadastrado = "O telefone já encontra-se cadastrado";
        public static string EmailJaCadastrado = "O email já encontra-se cadastrado";
        public static string CpfJaCadastrado = "O cpf já encontra-se cadastrado";

        #endregion

        #region Campo não encontrado
        public static string EmailNaoEncontrado = "Email não encontrado";

        #endregion

        #region Campo Invalido
        public static string EmailFormatoInvalido = "Email inválido";
        public static string SexoInvalido = "Informe uma das opções";
        #endregion
    }
}
