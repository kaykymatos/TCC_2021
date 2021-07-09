using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCCESTOQUE.Data;
using TCCESTOQUE.Interfaces.Repository;
using TCCESTOQUE.Models;

namespace TCCESTOQUE.Repository
{
    public class ChartRepository : IChartRepository
    {
        public readonly TCCESTOQUEContext _context;

        public ChartRepository(TCCESTOQUEContext context)
        {
            _context = context;
        }

        public VendaItensModel GetValoresSaida(Guid vendedorId)
        {
            IEnumerable<VendaItensModel> vendas = null;
            using (var connection = new MySqlConnection("Server = localhost; Database = TCCESTOQUE; Uid = root; Pwd ="))
            {
                var query = $"SELECT SUM(PrecoProduto*Quantidade) as PrecoProduto, SUM(CustoProduto*Quantidade) as CustoProduto FROM vendaitens right join venda on venda.VendaId = vendaitens.VendaId where venda.Cancelada = false and vendaitens.VendedorId = '{vendedorId}'" ;
                vendas = connection.Query<VendaItensModel>(query.ToString());
            }
            if(vendas != null)
            {
                foreach (var item in vendas)
                {
                    return item;
                }
            }
            return null;
        }

        //public List<dynamic> EntradaProduto()
        //{

        //}

        //public List<dynamic> SaidaProd() 
        //{

        //}
    }
}
