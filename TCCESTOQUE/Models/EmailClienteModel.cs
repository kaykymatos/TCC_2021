using System;
using System.ComponentModel.DataAnnotations;

namespace TCCESTOQUE.Models
{
    public class EmailClienteModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Informe o email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email não é valido")]
        public string Email { get; set; }
    }
}
