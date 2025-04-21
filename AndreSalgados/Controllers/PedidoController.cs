using Application.Core.Entities;
using Application.Core.Interfaces.Repositories;
using Infraestructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AndreSalgados.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoController(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var pedido = await Get();

            return View(pedido);
        }

        [HttpGet]
        public async Task<IEnumerable<Pedido>> Get()
        {
            var pedido = await _pedidoRepository.Get();

            return pedido;
        }
    }
}
