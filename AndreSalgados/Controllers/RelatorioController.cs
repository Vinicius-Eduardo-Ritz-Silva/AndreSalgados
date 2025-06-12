using Application.Core.DTOs;
using Application.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AndreSalgados.Controllers
{
    public class RelatorioController : Controller
    {
        private readonly IRelatorioVendaRepository _relatorioVendaRepository;

        public RelatorioController(IRelatorioVendaRepository relatorioVendaRepository)
        {
            _relatorioVendaRepository = relatorioVendaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IEnumerable<ProdutosMiasPedidosDTO>> ProdutosMaisPedidos()
        {
            var retorno = await _relatorioVendaRepository.ProdutosMaisPedidos();

            return retorno;
        }
    }
}
