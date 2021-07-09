using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCCESTOQUE.Models
{
    [Table("VendaItens")]
    public class VendaItensModel
    {
        [Key]
        public Guid VendaItensId { get; set; }

        [Required(ErrorMessage = "Informe a quantidade!")]
        public int Quantidade { get; set; }

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal PrecoProduto { get; set; }

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal CustoProduto { get; set; }

        [ForeignKey("Venda")]
        public Guid? VendaId { get; set; }
        public VendaModel Venda { get; set; }

        [ForeignKey("Carrinho")]
        [ScaffoldColumn(false)]
        public Guid? CarrinhoId { get; set; }
        public CarrinhoModel Carrinho { get; set; }

        [ForeignKey("Vendedor")]
        [ScaffoldColumn(false)]
        public Guid VendedorId { get; set; }
        public VendedorModel Vendedor { get; set; }

        [ForeignKey("Produto")]
        public Guid ProdutoId { get; set; }
        public ProdutoModel Produto { get; set; }
    }
}
