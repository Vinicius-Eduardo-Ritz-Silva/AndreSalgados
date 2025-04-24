using AndreSalgados.ViewModels;
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
            var produtos = await _produtoRepository.Get();
            var clientes = await _clienteRepository.Get();

            ViewBag.ProdutosDisponiveis = produtos.OrderBy(p => p.Descricao1);
            ViewBag.ProdutosPedido = new List<ProdutoPedido>();
            ViewBag.Clientes = clientes.OrderBy(c => c.Nome);

            if (Id == Guid.Empty)
            {
                var sessionPedidoId = HttpContext.Session.GetString("NovoPedidoId");

                if (!string.IsNullOrEmpty(sessionPedidoId))
                {
                    var novoPedido = new Pedido { Id = Guid.Parse(sessionPedidoId) };

                    return View(novoPedido);
                }
                else
                {
                    var novoPedido = new Pedido();

                    HttpContext.Session.SetString("NovoPedidoId", novoPedido.Id.ToString());

                    return View(novoPedido);
                }
            }
            else
            {
                var pedido = await GetPedidoById(Id);

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

        [HttpPost]
        public RetornoViewModel SalvarPedido(Guid Id, Guid ClienteId, bool Pago)
        {
            var pedido = new Pedido();

            pedido.Id = Id;
            pedido.Inclusao = DateTime.Now;
            pedido.Alteracao = DateTime.Now;
            pedido.Ativo = true;
            pedido.ClienteId = ClienteId;
            pedido.Pago = Pago;

            var retorno = _pedidoRepository.SalvarPedido(pedido);

            if (!Pago)
            {
                var cobranca = new Cobranca();

                pedido.CobrancaId = cobranca.Id;
            }
            
            return new RetornoViewModel
            {
                Sucesso = retorno,
                Mensagem = retorno ? "Deu bom!" : "Deu ruim!"
            };
        }

        [HttpPost]
        public RetornoViewModel AdicionarProdutoPedido(Guid Id, Guid ProdutoId, int Quantidade)
        {
            var retorno = _produtoPedidoRepository.AdicionarProdutoPedido(Id, ProdutoId, Quantidade);

            return new RetornoViewModel
            {
                Sucesso = retorno,
                Mensagem = retorno 
                    ? "Produto vinculado ao pedido com sucesso!" 
                    : "Erro ao vincular produto ao pedido!"
            };
        }

        [HttpPost]
        public RetornoViewModel RemoverProdutoPedido(Guid Id, Guid ProdutoId, int Quantidade)
        {
            return new RetornoViewModel
            {
                Sucesso = true,
                Mensagem = "Implementar Metodo Remover Produto Pedido"
            };
        }
    }
}
