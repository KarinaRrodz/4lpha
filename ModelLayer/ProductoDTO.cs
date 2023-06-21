using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace ModelLayer
{
    public class ProductoDTO
    {
       
        public int IdProducto { get; set; }
        [Required]
        [Range(1,int.MaxValue,ErrorMessage = "El campo {0} es requerido")]
        public int IdCategoria { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Nombre { get; set; } = null!;
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El campo {0} es requerido")]
        public int Precio { get; set; }
        public CategoriaDTO ? Categoria { get; set;}
    }
}
