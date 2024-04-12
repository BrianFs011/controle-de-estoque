using controle_estoque.Models;
using controle_estoque.Service;
using Microsoft.AspNetCore.Mvc;

namespace controle_estoque.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoContext _produtoContext;

        public ProdutoController(ProdutoContext produtoContext)
        {
            _produtoContext = produtoContext;
        }

        [HttpGet]
        [Route("GetListaProduto")]

        public async Task<IActionResult> GetProduto()
        {
            List<Produto> list = _produtoContext.Produto.ToList();
            return Ok(list);
        }

        [HttpPost]
        [Route("PostProduto")]
        public async Task<IActionResult> PostAddProduto([FromBody] ProdutoModel obj)
        {
            if (obj.nome != "" && obj.preco != 0 && obj.quantidade >= 0 && obj.descricao != "")
            {
                Produto produto = new Produto();
                produto.id = Guid.NewGuid().ToString();
                produto.nome = obj.nome;
                produto.preco = obj.preco;
                produto.quantidade = obj.quantidade;
                produto.descricao = obj.descricao;
                produto.img = obj.img;

                _produtoContext.Produto.Add(produto);
                _produtoContext.SaveChanges();

                return Ok(produto);
            }
            else { return BadRequest(); }
        }

        [HttpPost]
        [Route("PostAtualizaProduto")]
        public async Task<IActionResult> PostAtualizaProduto([FromBody] Produto obj)
        {
            if (obj.nome != "" && obj.preco != 0 && obj.quantidade >= 0 && obj.descricao != "" && obj.id != "")
            {

                List<Produto> list = _produtoContext.Produto.ToList();
                _produtoContext.Produto.Remove(list.Find(x => x.id == obj.id));

                Produto produto = new Produto();
                produto.id = obj.id;
                produto.nome = obj.nome;
                produto.preco = obj.preco;
                produto.quantidade = obj.quantidade;
                produto.descricao = obj.descricao;
                produto.img = obj.img;

                _produtoContext.Add(produto);
                _produtoContext.SaveChanges();

                return Ok(produto);
            } else { return BadRequest(); }
        }

        [HttpPost]
        [Route("PostProdutoDelete")]
        public async Task<IActionResult> PostDeletaProduto([FromBody] String id)
        {
            if (id != "")
            {
                List<Produto> list = _produtoContext.Produto.ToList();
                _produtoContext.Produto.Remove(list.Find(x => x.id == id));
                _produtoContext.SaveChanges();

                return Ok(list);
            } else { return BadRequest();  }
        }

        [HttpPost]
        [Route("PostTest")]
        public async Task<IActionResult> PostTestAddList([FromBody] List<ProdutoModel> obj)
        {
            Produto produto = new Produto();
            obj.ForEach(x => {
                if (x.nome != "" && x.preco != 0 && x.quantidade >= 0 && x.descricao != "")
                {
                    produto.id = Guid.NewGuid().ToString();
                    produto.nome = x.nome;
                    produto.preco = x.preco;
                    produto.quantidade = x.quantidade;
                    produto.descricao = x.descricao;
                    produto.img = x.img;

                    _produtoContext.Produto.Add(produto);
                    _produtoContext.SaveChanges();
                }
            });

            return Ok(produto);
        }
    }
}
