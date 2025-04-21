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

            return produto;
        }

        [HttpGet]
        public async Task<Produto> GetProdutoById(Guid Id)
        {
            var produto = await _produtoRepository.GetProdutoById(Id);

            return produto;
        }

        [HttpPost]
        public RetornoViewModel SalvarProduto([FromBody] string dados)
        {
            try
            {
                var jsonObj = JObject.Parse(dados);
                var produto = new Produto();
                var serializer = new JsonSerializer();
                serializer.Populate(jsonObj.CreateReader(), produto);

                produto.Alteracao = DateTime.Now;
                produto.Inclusao = DateTime.Now;
                produto.Ativo = true;

                var retorno = _produtoRepository.SalvarProduto(produto);

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
        public RetornoViewModel ExcluirProduto(Guid Id)
        {
            var retorno = _produtoRepository.ExcluirProduto(Id);

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
