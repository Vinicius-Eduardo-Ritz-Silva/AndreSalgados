using AndreSalgados.ViewModels;
using Application.Core.Entities;
using Application.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AndreSalgados.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var clientes = await Get();

            return View(clientes);
        }

        [HttpGet]
        public async Task<IActionResult> Detalhes(Guid Id)
        {
            var cliente = await GetClienteById(Id);

            return View(cliente);
        }

        [HttpGet]
        public async Task<IEnumerable<Cliente>> Get()
        {
            var clientes = await _clienteRepository.Get();

            return clientes;
        }

        [HttpGet]
        public async Task<Cliente> GetClienteById(Guid Id)
        {
            var cliente = await _clienteRepository.GetClienteById(Id);

            return cliente;
        }

        [HttpPost]
        public RetornoViewModel SalvarCliente(string dados)
        {
            try
            {
                var cliente = new Cliente(); //Realizar o populate object

                cliente.Alteracao = DateTime.Now;
                cliente.Inclusao = DateTime.Now;
                cliente.Ativo = true;

                var retorno = _clienteRepository.SalvarCliente(cliente);

                if (retorno)
                {
                    return new RetornoViewModel
                    {
                        Sucesso = true,
                        Mensagem = "Cliente cadastrado com sucesso!"
                    };
                }
                else
                {
                    return new RetornoViewModel
                    {
                        Sucesso = false,
                        Mensagem = "Erro ao cadastrar cliente!"
                    };
                }  
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        public RetornoViewModel ExcluirCliente(Guid Id)
        {
            var retorno = _clienteRepository.ExcluirCliente(Id);

            if (retorno)
            {
                return new RetornoViewModel
                {
                    Sucesso = true,
                    Mensagem = "Cliente excluido com sucesso!"
                };
            }
            else
            {
                return new RetornoViewModel
                {
                    Sucesso = false,
                    Mensagem = "Erro ao excluir cliente!"
                };
            }           
        }
    }
}
