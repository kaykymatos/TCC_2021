using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TCCESTOQUE.Models.Enum;

namespace TCCESTOQUE.Models
{
    [Table("Cliente")]
    public class ClienteModel
    {
        [Key]
        public Guid ClienteId { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Informe o nome")]
        public string Nome { get; set; }

        [StringLength(14)]
        public string Cpf { get; set; }

        [MaxLength(80)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe o Telefone!")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Informe o gênero!")]
        public SexoEnum Sexo { get; set; }

        [ScaffoldColumn(false)]
        public bool Inativo { get; set; }

        [ScaffoldColumn(false)]
        public ClienteEnderecoModel Endereco { get; set; }

        [ScaffoldColumn(false)]
        public ICollection<VendaModel> Venda { get; set; }

        [ForeignKey("Vendedor")]
        public Guid VendedorId { get; set; }
        public VendedorModel Vendedor { get; set; }

    }
}
