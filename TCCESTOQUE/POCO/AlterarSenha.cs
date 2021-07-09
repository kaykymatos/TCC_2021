using System;
using System.ComponentModel.DataAnnotations;

namespace TCCESTOQUE.POCO
{
    public class AlterarSenha
    {
        public Guid VendedorId { get; set; }
        public Guid TrocaId { get; set; }

        [Required(ErrorMessage = "Informe o código")]
        public int Codigo { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Informe a nova senha")]
        public string NovaSenha { get; set; }
    }
}
