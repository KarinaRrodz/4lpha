using ModelLayer;
using System.Net.Http.Json;

namespace CrudCliente.Services
{
    public class ProductoService : IProductoService
    {
        private readonly HttpClient _http;

        public ProductoService(HttpClient http)
        {
            _http = http;
        }
        public async Task<List<ProductoDTO>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<ProductoDTO>>>("api/Producto/Lista");

            if (result!.Correct)
                return result.Valor!;
            else
                throw new Exception(result.Message);
        }
        public async Task<ProductoDTO> Buscar(int Id)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<ProductoDTO>>($"api/Producto/Buscar/{Id}");

            if (result!.Correct)
                return result.Valor!;
            else
                throw new Exception(result.Message);
        }
        public async Task<int> Guardar(ProductoDTO producto)
        {
            var result = await _http.PostAsJsonAsync("api/Producto/Guardar", producto);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.Correct)
                return response.Valor!;
            else
                throw new Exception(response.Message);
        }

        public async Task<int> Editar(ProductoDTO producto)
        {
            var result = await _http.PutAsJsonAsync($"api/Producto/Editar/{producto.IdProducto}", producto);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.Correct)
                return response.Valor!;
            else
                throw new Exception(response.Message);
        }

        public async Task<bool> Eliminar(int Id)
        {
            var result = await _http.DeleteAsync($"api/Producto/Eliminar/{Id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.Correct)
                return response.Correct!;
            else
                throw new Exception(response.Message);
        }
    }
}
