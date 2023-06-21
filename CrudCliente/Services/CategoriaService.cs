using ModelLayer;
using System.Net.Http.Json;

namespace CrudCliente.Services
{
    public class CategoriaService:ICategoriaService
    {
        private readonly HttpClient _http;

        public CategoriaService(HttpClient http)
        {
            _http= http;
        }

        public async Task<List<CategoriaDTO>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<CategoriaDTO>>>("api/Categoria/Lista");

            if (result!.Correct)
                return result.Valor!;
            else
                throw new Exception(result.Message);
        }
    }
}
