using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Crud.Models;
using ModelLayer;
using Microsoft.EntityFrameworkCore;

namespace Crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly AlphaTechnologiesContext _dbcontext;

        public ProductoController(AlphaTechnologiesContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<ProductoDTO>>();
            var listaProductodto = new List<ProductoDTO>();

            try
            {
                foreach (var item in await _dbcontext.Productos.Include(d => d.IdCategoriaNavigation).ToListAsync())
                {
                    listaProductodto.Add(new ProductoDTO
                    {
                        IdProducto = item.IdProducto,
                        IdCategoria= item.IdCategoria,
                        Nombre = item.Nombre,
                        Precio = item.Precio,

                        Categoria = new CategoriaDTO
                        {
                            IdCategoria = item.IdCategoriaNavigation.IdCategoria,
                            NombreCategoria = item.IdCategoriaNavigation.NombreCategoria
                        }
                    });
                }
                responseApi.Correct = true;
                responseApi.Valor = listaProductodto;
            }
            catch (Exception ex)
            {
                responseApi.Correct = false;
                responseApi.Message = ex.Message;
            }
            return Ok(responseApi);

        }

        [HttpGet]
        [Route("Buscar/{Id}")]
        public async Task<IActionResult> Buscar(int Id)
        {
            var responseApi = new ResponseAPI<ProductoDTO>();
            var Productodto = new ProductoDTO();

            try
            {
                var dbProducto = await _dbcontext.Productos.FirstOrDefaultAsync(x => x.IdProducto == Id);
                if (dbProducto != null)
                {
                    Productodto.IdProducto = dbProducto.IdProducto;
                    Productodto.IdCategoria= dbProducto.IdCategoria;
                    Productodto.Nombre = dbProducto.Nombre;
                    Productodto.Precio = dbProducto.Precio;

                    responseApi.Correct = true;
                    responseApi.Valor = Productodto;
                    
                }
                else
                {
                    responseApi.Correct = false;
                    responseApi.Message = "No encontrado";
                }
            }
            catch (Exception ex)
            {
                responseApi.Correct = false;
                responseApi.Message = ex.Message;
            }
            return Ok(responseApi);
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar(ProductoDTO producto)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbProducto = new Producto
                {
                    Nombre = producto.Nombre,
                    IdCategoria = producto.IdCategoria,
                    Precio = producto.Precio,
                };

                _dbcontext.Productos.Add(dbProducto);
                await _dbcontext.SaveChangesAsync();

                if(dbProducto.IdProducto != 0)
                {
                    responseApi.Correct = true;
                    responseApi.Valor =dbProducto.IdProducto;
                }
                else
                {
                    responseApi.Correct = false;
                    responseApi.Message = "No registrado";
                }
            }
            catch (Exception ex)
            {
                responseApi.Correct = false;
                responseApi.Message = ex.Message;
            }
            return Ok(responseApi);
        }

        [HttpPut]
        [Route("Editar/{Id}")]
        public async Task<IActionResult> Editar(ProductoDTO producto, int Id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {

                var dbProducto = await _dbcontext.Productos.FirstOrDefaultAsync(e => e.IdProducto == Id);

                if (dbProducto != null)
                {

                    dbProducto.Nombre = producto.Nombre;
                    dbProducto.IdCategoria = producto.IdCategoria;
                    dbProducto.Precio = producto.Precio;


                    _dbcontext.Productos.Update(dbProducto);
                    await _dbcontext.SaveChangesAsync();


                    responseApi.Correct = true;
                    responseApi.Valor = dbProducto.IdProducto;


                }
                else
                {
                    responseApi.Correct = false;
                    responseApi.Message = "Empleado no econtrado";
                }

            }
            catch (Exception ex)
            {

                responseApi.Correct = false;
                responseApi.Message = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpDelete]
        [Route("Eliminar/{Id}")]
        public async Task<IActionResult> Eliminar(int Id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {

                var dbProducto = await _dbcontext.Productos.FirstOrDefaultAsync(e => e.IdProducto == Id);

                if (dbProducto != null)
                {
                    _dbcontext.Productos.Remove(dbProducto);
                    await _dbcontext.SaveChangesAsync();


                    responseApi.Correct = true;
                }
                else
                {
                    responseApi.Correct = false;
                    responseApi.Message = "Producto no econtrado";
                }

            }
            catch (Exception ex)
            {

                responseApi.Correct = false;
                responseApi.Message = ex.Message;
            }
            return Ok(responseApi);
        }
    }
}
