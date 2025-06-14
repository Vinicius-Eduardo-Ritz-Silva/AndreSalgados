using AndreSalgados.ViewModels;
using Application.Core.Entities;
using Application.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AndreSalgados.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var produto = await Get();

            return View(produto);
        }

        [HttpGet]
        public async Task<IActionResult> Detalhes(Guid Id)
        {
            var produto = await GetProdutoById(Id);

            return View(produto);
        }

        [HttpGet]
        public async Task<IEnumerable<Produto>> Get()
        {
            var produto = await _produtoRepository.Get();

            return produto.OrderBy(p => p.Descricao1);
        }

        [HttpGet]
        public async Task<Produto> GetProdutoById(Guid Id)
        {
            var produto = await _produtoRepository.GetById(Id);

            return produto;
        }

        [HttpPost]
        public async Task<RetornoViewModel> SalvarProduto([FromBody] string dados)
        {
            try
            {
                var produto = new Produto();

                JsonConvert.PopulateObject(dados, produto);

                var retornoValidar = await _produtoRepository.ValidarProduto(produto);

                if (!retornoValidar.Sucesso) 
                {
                    return new RetornoViewModel
                    {
                        Sucesso = retornoValidar.Sucesso,
                        Mensagem = retornoValidar.Mensagem
                    };
                }

                var retorno = await _produtoRepository.InsertOrReplace(produto);

                return new RetornoViewModel
                {
                    Sucesso = retorno,
                    Mensagem = retorno
                        ? "Produto cadastrado com sucesso!"
                        : "Erro ao cadastrar produto!"
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<RetornoViewModel> ExcluirProduto(Guid Id)
        {
            var retorno = await _produtoRepository.Excluir(Id);

            return new RetornoViewModel
            {
                Sucesso = retorno,
                Mensagem = retorno
                    ? "Produto excluido com sucesso!"
                    : "Erro ao excluir produto!"
            };
        }
    }
}
