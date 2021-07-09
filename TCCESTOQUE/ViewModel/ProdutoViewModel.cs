using System;
using System.ComponentModel.DataAnnotations;
using TCCESTOQUE.Models.Enum;

namespace TCCESTOQUE.ViewModel
{
    public class ProdutoViewModel
    {
        public Guid VendedorId { get; set; }
        public Guid FornecedorId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Insira o custo", AllowEmptyStrings = false)]
        public decimal Custo { get; set; }
        
        [Required(ErrorMessage = "Insira o valor", AllowEmptyStrings = false)]
        public decimal Valor { get; set; }
        
        [Required(ErrorMessage = "Insira a quantidade", AllowEmptyStrings = false)]
        public double Quantidade { get; set; }
        
        public UnidadeDeMedidaEnum UnidadeMedida { get; set; }
    }
}
