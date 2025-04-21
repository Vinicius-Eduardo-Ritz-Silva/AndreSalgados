using Application.Core.Entities;
using Application.Core.Interfaces.Repositories;
using Infraestructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AndreSalgados.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoPedidoRepository _produtoPedidoRepository;
        private readonly IClienteRepository _clienteRepository;

        public PedidoController(IPedidoRepository pedidoRepository,
                                IProdutoRepository produtoRepository,
                                IProdutoPedidoRepository produtoPedidoRepository,
                                IClienteRepository clienteRepository)
        {
            _pedidoRepository = pedidoRepository;
            _produtoRepository = produtoRepository;
            _produtoPedidoRepository = produtoPedidoRepository;
            _clienteRepository = clienteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var pedido = await Get();

            return View(pedido);
        }

        [HttpGet]
        public async Task<IActionResult> Detalhes(Guid Id)
        {
            if(Id == Guid.Empty)
            {
                var novoPedido = new Pedido();
                var produtos = await _produtoRepository.Get();
                var clientes = await _clienteRepository.Get();

                ViewBag.ProdutosDisponiveis = produtos;
                ViewBag.ProdutosPedido = new List<ProdutoPedido>();
                ViewBag.Clientes = clientes;

                return View(novoPedido);
            }
            else
            {
                var pedido = await GetPedidoById(Id);
                var produtos = await _produtoRepository.Get();
                var clientes = await _clienteRepository.Get();

                ViewBag.ProdutosDisponiveis = produtos;
                ViewBag.ProdutosPedido = new List<ProdutoPedido>();
                ViewBag.Clientes = clientes;

                return View(pedido);
            }
        }

        [HttpGet]
        public async Task<IEnumerable<Pedido>> Get()
        {
            var pedido = await _pedidoRepository.Get();

            return pedido;
        }

        [HttpGet]
        public async Task<Pedido> GetPedidoById(Guid Id)
        {
            var pedido = await _pedidoRepository.GetPedidoById(Id);

            return pedido;
        }
    }
}
