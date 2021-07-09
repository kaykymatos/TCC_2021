using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCCESTOQUE.Models
{
    [Table("Fornecedor")]
    public class FornecedorModel
    {
        [Key]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid FornecedorId { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Informe a RazãoSocial de usuario", AllowEmptyStrings = false)]
        public string RazaoSocial { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Informe a RazãoSocial de usuario", AllowEmptyStrings = false)]
        public string NomeFantasia { get; set; }

        [MaxLength(18)]
        [Required(ErrorMessage = "Informe a RazãoSocial de usuario", AllowEmptyStrings = false)]
        public string Cnpj { get; set; }

        [MaxLength(80)]
        [Required(ErrorMessage = "Informe o Email!")]
        public string Email { get; set; }

        [MaxLength(14)]
        public string Telefone { get; set; }

        [ScaffoldColumn(false)]
        public bool Inativo { get; set; }

        [ScaffoldColumn(false)]
        public FornecedorEnderecoModel Endereco { get; set; }

        [ScaffoldColumn(false)]
        public ICollection<EntradaModel> Entradas { get; set; }

        [ForeignKey("Vendedor")]
        public Guid VendedorId { get; set; }
        public VendedorModel Vendedor { get; set; }

    }
}
