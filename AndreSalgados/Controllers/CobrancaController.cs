using Application.Core.Entities;
using Application.Core.Interfaces.Repositories;
using Infraestructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AndreSalgados.Controllers
{
    public class CobrancaController : Controller
    {
        private readonly ICobrancaRepository _cobrancaRepository;

        public CobrancaController(ICobrancaRepository cobrancaRepository) 
        {
            _cobrancaRepository = cobrancaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cobranca = await Get();

            return View(cobranca);
        }

        public async Task<IEnumerable<Cobranca>> Get()
        {
            var cobranca = await _cobrancaRepository.GetWithInclude(p => p.Cliente);

            return cobranca.OrderBy(co => co.Cliente.Nome);
        }
    }
}
