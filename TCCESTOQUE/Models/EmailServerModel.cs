namespace TCCESTOQUE.Models
{
    public class EmailServerModel
    {
        public string EmailOrigem { get; set; }
        public string NomeOrigem { get; set; }
        public string Server { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Pass { get; set; }
        public string Retorno { get; set; }
        public bool Autentica { get; set; }
        public bool Ativo { get; set; }
        public string EmailDebug { get; set; }
    }
}
