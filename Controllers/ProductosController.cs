using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using API_PRODUCTO.Models;


namespace API_PRODUCTO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {

        private readonly BddAutomotoresContext _baseDatos;

        public ProductosController(BddAutomotoresContext baseDatos)
        {
            _baseDatos = baseDatos;
        }

        [HttpGet]
        [Route("lista")]
        public async Task<IActionResult> Lista()
        {
            var productos = await _baseDatos.Productos.ToListAsync();

            if (productos == null || !productos.Any())
            {
                return NotFound("NO SE ENCONTRARON PRODUCTOS.");
            }

            return Ok(productos);
        }


        // GET: api/productos/obtener/5
        [HttpGet]
        [Route("obtener/{id}")]
        public async Task<IActionResult> GetProducto(int id)
        {
            var producto = await _baseDatos.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound($"NO SE ENCONTRO EL PRODUCTO CON EL ID: {id}.");
            }

            return Ok(producto);
        }

        // PUT: api/productos/actualizar/5
        [HttpPut]
        [Route("actualizar/{id:int}")]
        public async Task<IActionResult> ActualizarProducto(int id, [FromBody] Producto producto)
        {
            if (id != producto.Id)
            {
                return BadRequest("EL ID DEL PRODUCTO NO COINCIDE CON EL ID ENVIADO.");
            }

            var productoExistente = await _baseDatos.Productos.FindAsync(id);

            if (productoExistente == null)
            {
                return NotFound($"NO SE ENCONTRO EL PRODUCTO CON ID: {id}.");
            }

            // Actualizar campos
            productoExistente.Nombre = producto.Nombre;
            productoExistente.Precio = producto.Precio;
            productoExistente.Cantidad = producto.Cantidad;

            try
            {
                await _baseDatos.SaveChangesAsync();
                return Ok("PRODUCTO ACTUALIZADO CORRECTAMENTE.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"ERROR AL ACTUALIZAR EL PRODUCTO: {ex.Message}");
            }
        }


        // DELETE: api/productos/eliminar/5
        [HttpDelete]
        [Route("eliminar/{id:int}")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            var producto = await _baseDatos.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound($"NO SE ENCONTRO EL PRODUCTO CON ID {id}.");
            }

            _baseDatos.Productos.Remove(producto);

            try
            {
                await _baseDatos.SaveChangesAsync();
                return Ok("PRODUCTO ELIMINADO CORRECTAMENTE.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"ERROR AL ELIMINAR EL PRODUCTO: {ex.Message}");
            }
        }

    }
}
