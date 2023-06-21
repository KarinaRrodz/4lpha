using System;
using System.Collections.Generic;

namespace Crud.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public int IdCategoria { get; set; }

    public string Nombre { get; set; } = null!;

    public int Precio { get; set; }

    public virtual Categorium IdCategoriaNavigation { get; set; } = null!;
}
//https://www.elmundo.es/traductor/