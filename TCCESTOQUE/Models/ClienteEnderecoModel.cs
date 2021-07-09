using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCCESTOQUE.Models
{
    [Table("ClienteEndereco")]
    public class ClienteEnderecoModel : EnderecoModel
    {
        [ForeignKey("Cliente")]
        public Guid ClienteId { get; set; }

        public ClienteModel Cliente { get; set; }

    }
}
