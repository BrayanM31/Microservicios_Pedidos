using Microsoft.AspNetCore.Mvc;
using ApiPedido.Data;
using ApiPedido.Models;
using Microsoft.AspNetCore.Authorization;  // ⬅️ AGREGAR

namespace ApiPedido.Controllers
{
    [ApiController]
    [Route("api/pedidos")]
    [Authorize]  // ⬅️ AGREGAR - Protege todo el controlador
    public class PedidoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PedidoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/pedidos
        [HttpGet]
        public IActionResult Get()
        {
            var pedidos = _context.Pedidos.ToList();
            return Ok(pedidos);
        }

        // POST: api/pedidos
        [HttpPost]
        public IActionResult Post(Pedido pedido)
        {
            pedido.Fecha = DateTime.UtcNow;
            _context.Pedidos.Add(pedido);
            _context.SaveChanges();
            return Ok(pedido);
        }
    }
}