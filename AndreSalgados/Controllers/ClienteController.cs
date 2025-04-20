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

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IEnumerable<Cliente> Get()
        {
            var clientes = _clienteRepository.Get();

            return clientes;
        }

        [HttpGet]
        public Cliente GetClienteById(Guid Id)
        {
            var cliente = _clienteRepository.GetClienteById(Id);

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
