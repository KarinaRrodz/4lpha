using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Crud.Models;
using ModelLayer;
using Microsoft.EntityFrameworkCore;

namespace CrudServidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly AlphaTechnologiesContext _dbcontext;

        public CategoriaController(AlphaTechnologiesContext dbcontext)
        {
            _dbcontext= dbcontext;
        }

        [HttpGet]
        [Route ("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<CategoriaDTO>>();
            var listaCategoriadto = new List<CategoriaDTO>();

            try
            {
                foreach (var item in await _dbcontext.Categoria.ToListAsync())
                {
                    listaCategoriadto.Add(new CategoriaDTO
                    {
                        IdCategoria = item.IdCategoria,
                        NombreCategoria = item.NombreCategoria,
                    });
                }
                responseApi.Correct = true;
                responseApi.Valor= listaCategoriadto;
            }catch(Exception ex)
            {
                responseApi.Correct = false;
                responseApi.Message = ex.Message;
            }
            return Ok (responseApi);
        }
    }
}
