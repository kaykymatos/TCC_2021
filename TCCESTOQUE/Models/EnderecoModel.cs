using System;
using System.ComponentModel.DataAnnotations;
using TCCESTOQUE.Models.Enum;

namespace TCCESTOQUE.Models
{
    public class EnderecoModel
    {
        [Key]
        public Guid EnderecoId { get; set; }

        [StringLength(9)]
        [Required(ErrorMessage = "Informe o Cep", AllowEmptyStrings = false)]
        public string Cep { get; set; }

        [MaxLength(80)]
        [Required(ErrorMessage = "Informe o Logradouro", AllowEmptyStrings = false)]
        public string Logradouro { get; set; }

        [MaxLength(80)]
        public string Complemento { get; set; }

        [MaxLength(6)]
        [Required(ErrorMessage = "Informe o numero")]
        public string Numero { get; set; }

        [MaxLength(80)]
        [Required(ErrorMessage = "Informe o Bairro", AllowEmptyStrings = false)]
        public string Bairro { get; set; }

        [MaxLength(80)]
        [Required(ErrorMessage = "Informe a Localidade", AllowEmptyStrings = false)]
        public string Localidade { get; set; }

        public UnidadeFederalEnum Uf { get; set; }
    }
}
