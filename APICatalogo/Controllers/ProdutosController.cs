using APICatalogo.Models;
using APICatalogo.Repositories;
using APICatalogo.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly ILogger<ProdutosController> _logger;

    public ProdutosController(IProdutoRepository produtoRepository, ILogger<ProdutosController> logger)
    {
        _produtoRepository = produtoRepository;
        _logger = logger;
    }

    [HttpGet("produtoPorCategoria/{id}")]
    public ActionResult<IEnumerable<Produto>> GetProdutosPorCategoria(int id)
    {
        var produtos = _produtoRepository.GetProdutosPorCategoria(id);
        if (produtos.Count().Equals(0))
        {
            _logger.LogWarning($"Produto com a CatergoriaId = {id} não encontrado...");
            return NotFound($"Produto com a CatergoriaId = {id} não encontrado...");
        }
        return Ok(produtos);
    }

    [HttpGet]
    public ActionResult<IEnumerable<Produto>> Get()
    {
        var produtos = _produtoRepository.GetAll();
        if (produtos is null) { return NotFound(); }
        return Ok(produtos);
    }

    [HttpGet("{id}", Name = "ObterProduto")]
    public ActionResult<Produto> Get(int id)
    {
        var produto = _produtoRepository.Get(p => p.ProdutoId == id);
        if (produto is null)
        {
            _logger.LogWarning($"Produto com id = {id} não encontrado...");
            return NotFound($"Produto com id = {id} não encontrado...");
        }
        return Ok(produto);
    }

    [HttpPost]
    public ActionResult Create(Produto produto)
    {
        if (produto is null)
            return BadRequest();

        var novoProduto = _produtoRepository.Create(produto);

        return new CreatedAtRouteResult("ObterProduto", new { id = novoProduto.ProdutoId }, novoProduto);
    }

    [HttpPut("{id:int}")]
    public ActionResult Update(int id, Produto produto)
    {
        if (id != produto.ProdutoId)
        {
            return BadRequest();
        }

        var produtoAtualizado = _produtoRepository.Update(produto);
        return Ok(produtoAtualizado);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var produto = _produtoRepository.Get(p => p.ProdutoId == id);
        if (produto is null) { return NotFound("Produto não encontrado."); }
        var produtoDeletado = _produtoRepository.Delete(produto);
        return Ok(produtoDeletado);

    }
}
