using System;
using TCCESTOQUE.Models.Enum;

namespace TCCESTOQUE.ViewModel
{
    public class ClienteViewModel
    {

        #region Cliente
        public Guid ClienteId { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }

        public SexoEnum Sexo { get; set; }

        public Guid VendedorId { get; set; }
        #endregion

        #region Endereco
        public Guid EnderecoId { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public UnidadeFederalEnum Uf { get; set; }
        #endregion
    }
}
