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
        private readonly ICobrancaRepository _cobrancaRepository;

        public PedidoController(IPedidoRepository pedidoRepository,
                                IProdutoRepository produtoRepository,
                                IProdutoPedidoRepository produtoPedidoRepository,
                                IClienteRepository clienteRepository,
                                ICobrancaRepository cobrancaRepository)
        {
            _pedidoRepository = pedidoRepository;
            _produtoRepository = produtoRepository;
            _produtoPedidoRepository = produtoPedidoRepository;
            _clienteRepository = clienteRepository;
            _cobrancaRepository = cobrancaRepository;
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
            ViewBag.Clientes = clientes.OrderBy(c => c.Nome);

            if (Id == Guid.Empty)
            {
                ViewBag.ProdutosPedido = new List<ProdutoPedido>();

                var novoPedido = new Pedido { Id = Guid.Empty };

                return View(novoPedido);
                
            }
            else
            {
                ViewBag.ProdutosPedido = _produtoPedidoRepository.GetProdutoByPedido(Id);

                var pedido = await GetPedidoById(Id);

                return View(pedido);
            }
        }

        [HttpGet]
        public async Task<IEnumerable<Pedido>> Get()
        {
            var pedido = await _pedidoRepository.GetWithInclude(p => p.Cliente);

            return pedido.OrderBy(p => p.Cliente.Nome);
        }

        [HttpGet]
        public async Task<Pedido> GetPedidoById(Guid Id)
        {
            var pedido = await _pedidoRepository.GetPedidoById(Id);

            return pedido;
        }

        [HttpPost]
        public RetornoViewModel SalvarPedido(Guid Id, Guid ClienteId, bool Pago, bool Edicao)
        {
            var pedido = new Pedido
            {
                Id = Id,
                Inclusao = DateTime.Now,
                Alteracao = DateTime.Now,
                Ativo = true,
                ClienteId = ClienteId,
                Pago = Pago ? Pedido.PedidoStatus.Pago : Pedido.PedidoStatus.Pendente,
                Edicao = Edicao
            };

            if (pedido.Pago == Pedido.PedidoStatus.Pago)
                pedido.Status = Pedido.CobrancaStatus.PagoNaHora;

            var retornoSalvar = _pedidoRepository.SalvarPedido(pedido);

            if (!retornoSalvar)
                return new RetornoViewModel
                {
                    Sucesso = false,
                    Mensagem = "Erro ao salvar pedido!",
                };

            var retornoValidar = ValidarPedidoPago(pedido);

            if (!retornoValidar.Sucesso)
                return retornoValidar;

            return new RetornoViewModel
            {
                Sucesso = true,
                Mensagem = pedido.Edicao
                    ? "Pedido em edição!"
                    : "Pedido salvo com sucesso!",
                Dados = new
                {
                    pedidoId = pedido.Id
                }
            };
        }

        [HttpPost]
        public async Task<RetornoViewModel> FecharPedido(Guid Id)
        {
            var pedido = await _pedidoRepository.GetById(Id);

            pedido.Pago = Pedido.PedidoStatus.Fechado;

            var retorno = await _pedidoRepository.InsertOrReplace(pedido);

            return new RetornoViewModel
            {
                Sucesso = true,
                Mensagem = retorno
                    ? "Pedido finalizado com sucesso!"
                    : "Erro ao finalizar pedido!",
                Dados = new
                {
                    pedidoId = pedido.Id
                }
            };
        }

        [HttpPost]
        public async Task<RetornoViewModel> AdicionarProdutoPedido(Guid Id, Guid ProdutoId, int Quantidade)
        {
            var pedido = await _pedidoRepository.GetById(Id);

            if (pedido == null || pedido.ClienteId == Guid.Empty)
            {
                return new RetornoViewModel
                {
                    Sucesso = false,
                    Mensagem = "Salve o pedido com um cliente antes de adicionar produtos."
                };
            }

            var produtoExistente = _produtoPedidoRepository.ProdutoExisteNoPedido(Id, ProdutoId);

            if (produtoExistente)
            {
                return new RetornoViewModel
                {
                    Sucesso = false,
                    Mensagem = "Este produto já está no pedido. Atualize a quantidade se necessário."
                };
            }

            var retornoValidarQuantidade = _produtoPedidoRepository.ValidarQuantidadeProdutoPedido(Quantidade);

            if (!retornoValidarQuantidade)
            {
                return new RetornoViewModel
                {
                    Sucesso = false,
                    Mensagem = "A quantidade deve ser maior que zero"
                };
            }

            var retornoAdicionarProduto = _produtoPedidoRepository.AdicionarProdutoPedido(Id, ProdutoId, Quantidade);

            if (!retornoAdicionarProduto.Sucesso)
                return new RetornoViewModel
                {
                    Sucesso = false,
                    Mensagem = "Erro ao vincular produto ao pedido!"
                };

            var retornoValidar = ValidarPedidoPago((Pedido)retornoAdicionarProduto.Dados);

            if (!retornoValidar.Sucesso)
                return retornoValidar;

            return new RetornoViewModel
            {
                Sucesso = true,
                Mensagem = "Produto vinculado ao pedido com sucesso!"
            };
        }

        [HttpPost]
        public RetornoViewModel AtualizarQuantidadeProdutoPedido(Guid Id, int Quantidade)
        {
            var retornoValidarQuantidade = _produtoPedidoRepository.ValidarQuantidadeProdutoPedido(Quantidade);

            if (!retornoValidarQuantidade)
            {
                return new RetornoViewModel
                {
                    Sucesso = false,
                    Mensagem = "A quantidade deve ser maior que zero"
                };
            }

            var retornoAtualizar = _produtoPedidoRepository.AtualizarQuantidadeProdutoPedido(Id, Quantidade);

            if (!retornoAtualizar)
                return new RetornoViewModel
                {
                    Sucesso = false,
                    Mensagem = "Erro ao vincular produto ao pedido!"
                };

            var pedido = _pedidoRepository.GetPedidoByProdutoPedido(Id);

            var retornoValidar = ValidarPedidoPago(pedido);

            if (!retornoValidar.Sucesso)
                return retornoValidar;

            return new RetornoViewModel
            {
                Sucesso = true,
                Mensagem = "Produto vinculado ao pedido com sucesso!"
            };
        }

        [HttpPost]
        public RetornoViewModel RemoverProdutoPedido(Guid Id)
        {
            var retornoRemover = _produtoPedidoRepository.RemoverProdutoPedido(Id);

            if (!retornoRemover)
                return new RetornoViewModel
                {
                    Sucesso = false,
                    Mensagem = "Deu ruim!"
                };

            var pedido = _pedidoRepository.GetPedidoByProdutoPedido(Id);

            var retornoValidar = ValidarPedidoPago(pedido);

            if (!retornoValidar.Sucesso)
                return retornoValidar;

            return new RetornoViewModel
            {
                Sucesso = true,
                Mensagem = "Deu bom!"
            };
        }

        [HttpPost]
        public async Task<RetornoViewModel> ExcluirPedido(Guid Id)
        {
            var retornoExcluir = await _pedidoRepository.Excluir(Id);

            if (!retornoExcluir)
                return new RetornoViewModel
                {
                    Sucesso = false,
                    Mensagem = "Deu ruim!"
                };

            var pedido = await GetPedidoById(Id);

            var retornoValidar = ValidarPedidoPago(pedido);

            if (!retornoValidar.Sucesso)
                return retornoValidar;

            return new RetornoViewModel
            {
                Sucesso = true,
                Mensagem = "Deu bom!"
            };
        }

        public RetornoViewModel ValidarPedidoPago(Pedido pedido)
        {
            if (pedido.Pago == Pedido.PedidoStatus.Pendente)
            {
                var retornoValidar = _cobrancaRepository.GerarCobranca(pedido);

                if (!retornoValidar)
                    return new RetornoViewModel
                    {
                        Sucesso = false,
                        Mensagem = "Erro ao gerar cobrança!",
                    };
            }
            return new RetornoViewModel
            {
                Sucesso = true,
                Mensagem = "Cobrança gerada com sucesso!",
            };
        }
    }
}
