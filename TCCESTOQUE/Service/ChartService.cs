using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCCESTOQUE.Interfaces.Repository;
using TCCESTOQUE.Interfaces.Service;
using TCCESTOQUE.Models;

namespace TCCESTOQUE.Service
{
    public class ChartService : IChartService
    {
        private readonly IChartRepository _chartRepository;
        public ChartService(IChartRepository chartRepository)
        {
            _chartRepository = chartRepository;
        }

        public decimal GetValorTotalCusto(Guid vendedorId)
        {
            return _chartRepository.GetValoresSaida(vendedorId).CustoProduto;
        }

        public decimal GetValorTotalValor(Guid vendedorId)
        {
            return _chartRepository.GetValoresSaida(vendedorId).PrecoProduto;
        }
    }
}
