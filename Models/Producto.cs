using System;
using System.Collections.Generic;

namespace API_PRODUCTO.Models;

public partial class Producto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Precio { get; set; } = null!;

    public int Cantidad { get; set; }
}
