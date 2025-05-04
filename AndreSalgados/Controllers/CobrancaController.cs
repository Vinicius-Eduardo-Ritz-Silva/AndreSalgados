using AndreSalgados.ViewModels;
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

            return cobranca.OrderBy(co => co.Valor);
        }

        [HttpPost]
        public RetornoViewModel QuitarCobranca(Guid id)
        {
            var retorno = _cobrancaRepository.QuitarCobranca(id);

            return new RetornoViewModel
            {
                Sucesso = retorno,
                Mensagem = retorno 
                    ? "Cobranca quitada com sucesso!" 
                    : "Erro ao quitar cobranca!"
            };
        }

        [HttpPost]
        public RetornoViewModel DefinirDataCobranca(Guid id, DateTime dataCobranca)
        {
            var retorno = _cobrancaRepository.DefinirDataCobranca(id, dataCobranca);

            return new RetornoViewModel
            {
                Sucesso = retorno,
                Mensagem = retorno 
                    ? "Data definida com sucesso!" 
                    : "Erro ao definir a data!"
            };
        }

        [HttpPost]
        public RetornoViewModel MarcarComoPerdida(Guid id)
        {
            var retorno = _cobrancaRepository.MarcarComoPerdida(id);

            return new RetornoViewModel
            {
                Sucesso = retorno,
                Mensagem = retorno 
                    ? "Cobrança marcada como perdida!" 
                    : "Erro ao marcar mudança como perdida!"
            };
        }
    }
}
