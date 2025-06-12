using AndreSalgados.ViewModels;
using Application.Core.Entities;
using Application.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

            return clientes.OrderBy(c => c.Nome);
        }

        [HttpGet]
        public async Task<Cliente> GetClienteById(Guid Id)
        {
            var cliente = await _clienteRepository.GetById(Id);

            return cliente;
        }

        [HttpPost]
        public async Task<RetornoViewModel> SalvarCliente([FromBody] string dados)
        {
            try
            {
                var cliente = new Cliente();

                JsonConvert.PopulateObject(dados, cliente);

                cliente.Alteracao = DateTime.Now;
                cliente.Inclusao = DateTime.Now;
                cliente.Ativo = true;

                var retornoValidar = await _clienteRepository.ValidarNumero(cliente);

                if (!retornoValidar.Sucesso)
                    return new RetornoViewModel
                    {
                        Sucesso = retornoValidar.Sucesso,
                        Mensagem = retornoValidar.Mensagem
                    };

                var retorno = await _clienteRepository.InsertOrReplace(cliente);

                return new RetornoViewModel
                {
                    Sucesso = retorno,
                    Mensagem = retorno 
                        ? "Cliente cadastrado com sucesso!" 
                        : "Erro ao cadastrar cliente!"
                };  
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<RetornoViewModel> ExcluirCliente(Guid Id)
        {
            var retorno = await _clienteRepository.Excluir(Id);

            return new RetornoViewModel
            {
                Sucesso = retorno,
                Mensagem = retorno 
                    ? "Cliente excluido com sucesso!" 
                    : "Erro ao excluir cliente!"
            };
        }
    }
}
