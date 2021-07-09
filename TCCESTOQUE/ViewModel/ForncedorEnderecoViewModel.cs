using System;
using TCCESTOQUE.Models.Enum;

namespace TCCESTOQUE.ViewModel
{
    public class FornecedorEnderecoViewModel
    {
        #region Fornecedor
        public Guid FornecedorId { get; set; }
        public string NomeFantasia { get; set; }
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
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
