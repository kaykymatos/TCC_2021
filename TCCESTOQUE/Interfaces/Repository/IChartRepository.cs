using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCCESTOQUE.Models;

namespace TCCESTOQUE.Interfaces.Repository
{
    public interface IChartRepository
    {
        public VendaItensModel GetValoresSaida(Guid vendedorId);
    }
}
