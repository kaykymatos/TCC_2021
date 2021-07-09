using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TCCESTOQUE.Models.Enum;

namespace TCCESTOQUE.Models
{
    [Table("Vendedor")]
    public class VendedorModel
    {
        [Key]
        [ScaffoldColumn(false)]
        public Guid VendedorId { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Informe o nome de usuario", AllowEmptyStrings = false)]
        public string Nome { get; set; }

        [MaxLength(70)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Informe a senha", AllowEmptyStrings = false)]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Informe a data de nascimento", AllowEmptyStrings = false)]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        [MaxLength(80)]
        [Required(ErrorMessage = "Informe o Email")]
        public string Email { get; set; }

        [MaxLength(14)]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Escolha uma das opções")]

        public SexoEnum Sexo { get; set; }

        [NotMapped]
        public bool Logar { get; set; }

        [ScaffoldColumn(false)]
        public bool Inativo { get; set; }
    }
}
