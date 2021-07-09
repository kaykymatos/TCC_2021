using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCCESTOQUE.Models
{
    [Table("Venda")]
    public class VendaModel
    {
        [Key]
        public Guid VendaId { get; set; }

        [Required(ErrorMessage = "Data de venda é obrigatoria!", AllowEmptyStrings = false)]
        public DateTime DataVenda { get; set; }

        [ScaffoldColumn(false)]
        public bool Cancelada { get; set; }

        [ForeignKey("Vendedor")]
        public Guid VendedorId { get; set; }
        [ScaffoldColumn(false)]
        public VendedorModel Vendedor { get; set; }

        [ForeignKey("Cliente")]
        public Guid ClienteId { get; set; }
        [ScaffoldColumn(false)]
        public ClienteModel Cliente { get; set; }

        public ICollection<VendaItensModel> Itens { get; set; }

        public decimal Valor()
        {
            var valor = 0m;
            foreach (var item in Itens)
            {
                valor += item.Produto.Valor * item.Quantidade;
            }
            return valor;
        }

        public string PegarData(DateTime data)
        {
            return data.ToShortDateString();
        }
    }
}
