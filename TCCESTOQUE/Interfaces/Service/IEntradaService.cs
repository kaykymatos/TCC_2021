using TCCESTOQUE.Models;

namespace TCCESTOQUE.Interfaces.Service
{
    public interface IEntradaService : IBaseService<EntradaModel>
    {
        public void PostEntrada(EntradaModel entrada);

        public string CancelarEntrada(EntradaModel entrada);
    }
}
