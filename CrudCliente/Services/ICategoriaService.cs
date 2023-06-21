using ModelLayer;

namespace CrudCliente.Services
{
    public interface ICategoriaService
    {
        Task<List<CategoriaDTO>> Lista();
    }
}
