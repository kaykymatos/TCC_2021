using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCCESTOQUE.Models
{
    [Table("AlterarSenha")]
    public class AlterarSenhaModel
    {
        [Key]
        public Guid Id { get; set; }

        public int Codigo { get; set; }

        public DateTime DataEmissão { get; set; }

        public bool Invalida { get; set; }

        [ForeignKey("Vendedor")]
        public Guid? VendedorId { get; set; }

        public VendedorModel Vendedor { get; set; }
    }
}
