using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TCCESTOQUE.Models.Enum;

namespace TCCESTOQUE.Models
{
    [Table("Entrada")]
    public class EntradaModel
    {
        [Key]
        public Guid EntradaId { get; set; }

        [ScaffoldColumn(false)]
        [Column(TypeName = "decimal(12,2)")]
        public decimal Valor { get; set; }

        [ScaffoldColumn(false)]
        [Column(TypeName = "decimal(12,2)")]
        public decimal Custo { get; set; }

        [Required(ErrorMessage = "Informe a quantidade")]
        public double Quantidade { get; set; }

        [Required]
        public UnidadeDeMedidaEnum UnidadeMedida { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DataEntrada { get; set; } = DateTime.Now;

        [ScaffoldColumn(false)]
        public bool Cancelada { get; set; }

        [ForeignKey("Produto")]
        public Guid ProdutoId { get; set; }
        public ProdutoModel Produto { get; set; }

        [ForeignKey("Fornecedor")]
        [Required(ErrorMessage = "Selecione um fornecedor")]
        public Guid FornecedorId { get; set; }
        public FornecedorModel Fornecedor { get; set; }

        [ForeignKey("Vendedor")]
        public Guid VendedorId { get; set; }
        public VendedorModel Vendedor { get; set; }
    }
}
