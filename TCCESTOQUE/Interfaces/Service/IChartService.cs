using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TCCESTOQUE.Interfaces.Service
{
    public interface IChartService
    {
        public decimal GetValorTotalCusto(Guid vendedorId);

        public decimal GetValorTotalValor(Guid vendedorId);
    }
}
