using ModelLayer;

namespace CrudCliente.Services
{
    public interface IProductoService
    {
        Task<List<ProductoDTO>> Lista();
        Task<ProductoDTO> Buscar(int Id);
        Task<int> Guardar(ProductoDTO producto);
        Task<int> Editar(ProductoDTO producto);
        Task<bool> Eliminar(int Id);
    }
}
