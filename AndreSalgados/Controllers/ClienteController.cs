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
    }
}
