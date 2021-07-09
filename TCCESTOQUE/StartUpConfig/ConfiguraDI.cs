using Microsoft.Extensions.DependencyInjection;
using TCCESTOQUE.AutoMapper;
using TCCESTOQUE.Interfaces.Repository;
using TCCESTOQUE.Interfaces.Service;
using TCCESTOQUE.Repository;
using TCCESTOQUE.Service;

namespace TCCESTOQUE.StartUpOpcoes
{
    public class ConfiguraDI
    {
        public static void ConfigureAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile));
        }

        public static void ConfigureServicesAndRepo(IServiceCollection services)
        {
            services.AddScoped<IVendedorService, VendedorService>();
            services.AddScoped<IVendedorRepository, VendedorRepository>();

            services.AddScoped<ICarrinhoRepository, CarrinhoRepository>();
            services.AddScoped<ICarrinhoService, CarrinhoService>();

            services.AddScoped<IFornecedorService, FornecedorService>();
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<IFornecedorEnderecoRepository, FornecedorEnderecoRepository>();

            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();

            services.AddScoped<IVendaService, VendaService>();
            services.AddScoped<IVendaRepository, VendaRepository>();

            services.AddScoped<IVendaItensService, VendaItensService>();
            services.AddScoped<IVendaItensRepository, VendaItensRepository>();

            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IClienteEnderecoRepository, ClienteEnderecoRepository>();

            services.AddScoped<IEntradaService, EntradaService>();
            services.AddScoped<IEntradaRepository, EntradaRepository>();

            services.AddScoped<IMovimentacaoService, MovimentacaoService>();

            services.AddScoped<ISelectListService, SelectListService>();

            services.AddScoped<ISenhaRepository, SenhaRepository>();

            services.AddScoped<IChartRepository, ChartRepository>();
            services.AddScoped<IChartService, ChartService>();
        }
    }
}
