using Lab8_BryanCama.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab8Bryan.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LinqController : ControllerBase
    {
        private readonly LinqexampleContext _context;

        public LinqController(LinqexampleContext context)
        {
            _context = context;
        }

        // Ejercicio 1: Obtener los Clientes que Tienen un Nombre Específico
        [HttpGet("clientes/nombre/{nombre}")]
        public async Task<IActionResult> GetClientesPorNombre(string nombre)
        {
            var clientes = await _context.Clients
                .Where(c => c.Name.Contains(nombre))
                .ToListAsync();

            if (clientes.Count == 0)
            {
                return NotFound($"No se encontraron clientes con el nombre que contiene: {nombre}");
            }

            return Ok(clientes);
        }
        // Ejercicio 2: Obtener los Productos con Precio Mayor a un Valor Específico
        [HttpGet("productos/precio/{minPrecio}")]
        public async Task<IActionResult> GetProductosConPrecioMayor(decimal minPrecio)
        {
            var productos = await _context.Products
                .Where(p => p.Price > minPrecio)
                .ToListAsync();

            if (productos.Count == 0)
            {
                return NotFound($"No se encontraron productos con precio mayor a {minPrecio}.");
            }

            return Ok(productos);
        }
        // Ejercicio 3: Obtener el Detalle de los Productos en una Orden
        [HttpGet("orden/productos/{orderId}")]
        public async Task<IActionResult> GetProductosEnOrden(int orderId)
        {
            var productos = await _context.Orders
                .Where(o => o.OrderId == orderId)
                .Select(o => new
                {
                    o.OrderId,
                    Producto = o.Product.Name,
                    o.Product.Price,
                    o.OrderDate
                })
                .ToListAsync();

            if (productos.Count == 0)
            {
                return NotFound($"No se encontraron productos en la orden con ID {orderId}.");
            }

            return Ok(productos);
        }
        // Ejercicio 4: Obtener la Cantidad Total de Productos por Orden
        [HttpGet("orden/cantidad-total/{orderId}")]
        public async Task<IActionResult> GetCantidadTotalPorOrden(int orderId)
        {
            var cantidad = await _context.Orders
                .Where(o => o.OrderId == orderId)
                .CountAsync(); 

            return Ok(new { OrderId = orderId, CantidadTotal = cantidad });
        }
        // Ejercicio 5: Obtener el Producto Más Caro
            [HttpGet("producto-mas-caro")]
        public async Task<IActionResult> GetProductoMasCaro()
        {
            var producto = await _context.Products
                .OrderByDescending(p => p.Price)
                .FirstOrDefaultAsync();

            return producto != null ? Ok(producto) : NotFound("No hay productos.");
        }
        // Ejercicio 6: Obtener Todos los Pedidos Realizados Después de una Fecha
        [HttpGet("pedidos/desde/{fecha}")]
        public async Task<IActionResult> GetPedidosDesdeFecha(DateTime fecha)
        {
            var pedidos = await _context.Orders
                .Where(o => o.OrderDate > fecha)
                .ToListAsync();

            return pedidos.Count > 0 ? Ok(pedidos) : NotFound("No hay pedidos después de esa fecha.");
        }
        // Ejercicio 7: Obtener el Promedio de Precio de los Productos
        [HttpGet("productos/precio-promedio")]
        public async Task<IActionResult> GetPrecioPromedio()
        {
            var promedio = await _context.Products
                .Select(p => p.Price)
                .AverageAsync();

            return Ok(new { PrecioPromedio = promedio });
        }
        // Ejercicio 8: Obtener Todos los Productos que No Tienen Descripción
        [HttpGet("productos/sin-descripcion")]
        public async Task<IActionResult> GetProductosSinDescripcion()
        {
            var productos = await _context.Products
                .Where(p => string.IsNullOrEmpty(p.Description))
                .ToListAsync();

            return productos.Count > 0 ? Ok(productos) : NotFound("No hay productos sin descripción.");
        }
        // Ejercicio 9: Obtener el Cliente con Mayor Número de Pedidos
        [HttpGet("cliente-mas-pedidos")]
        public async Task<IActionResult> GetClienteConMasPedidos()
        {
            var resultado = await _context.Orders
                .GroupBy(o => o.ClientId)
                .Select(g => new {
                    ClientId = g.Key,
                    CantidadPedidos = g.Count()
                })
                .OrderByDescending(x => x.CantidadPedidos)
                .FirstOrDefaultAsync();

            if (resultado == null) return NotFound("No se encontraron pedidos.");

            var cliente = await _context.Clients.FindAsync(resultado.ClientId);
            return Ok(new {
                cliente.ClientId,
                cliente.Name,
                resultado.CantidadPedidos
            });
        }
        // Ejercicio 10: Obtener Todos los Pedidos y sus Detalles
        [HttpGet("pedidos/detalles")]
        public async Task<IActionResult> GetPedidosYDetalles()
        {
            var detalles = await _context.Orders
                .Select(o => new {
                    o.OrderId,
                    Producto = o.Product.Name,
                    Precio = o.Product.Price,
                    Cliente = o.Client.Name,
                    Fecha = o.OrderDate
                })
                .ToListAsync();

            return Ok(detalles);
        }
        // Ejercicio 11: Obtener Todos los Productos Vendidos por un Cliente Específico
        [HttpGet("productos/cliente/{clientId}")]
        public async Task<IActionResult> GetProductosPorCliente(int clientId)
        {
            var productos = await _context.Orders
                .Where(o => o.ClientId == clientId)
                .Select(o => o.Product.Name)
                .Distinct()
                .ToListAsync();

            return productos.Count > 0 ? Ok(productos) : NotFound("No se encontraron productos para el cliente.");
        }
        // Ejercicio 12: Obtener Todos los Clientes que Han Comprado un Producto Específico
        [HttpGet("clientes/producto/{productId}")]
        public async Task<IActionResult> GetClientesPorProducto(int productId)
        {
            var clientes = await _context.Orders
                .Where(o => o.ProductId == productId)
                .Select(o => o.Client.Name)
                .Distinct()
                .ToListAsync();

            return clientes.Count > 0 ? Ok(clientes) : NotFound("No se encontraron clientes que hayan comprado ese producto.");
        }

    }
}