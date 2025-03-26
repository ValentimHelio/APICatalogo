using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IUnitOfWork _iof;
    private readonly IMapper _mapper;
    private readonly ILogger<ProdutosController> _logger;

    public ProdutosController(IUnitOfWork iof, IMapper mapper, ILogger<ProdutosController> logger)
    {
        _iof = iof;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet("produtoPorCategoria/{id}")]
    public ActionResult<IEnumerable<ProdutoDTO>> GetProdutosPorCategoria(int id)
    {
        var produtos = _iof.ProdutoRepository.GetProdutosPorCategoria(id);
        if (produtos.Count().Equals(0))
        {
            _logger.LogWarning($"Produto com a CatergoriaId = {id} não encontrado...");
            return NotFound($"Produto com a CatergoriaId = {id} não encontrado...");
        }
        // var destino = _mapper.Map<Destino>(origem);
        var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
        return Ok(produtosDto);
    }

    [HttpGet]
    public ActionResult<IEnumerable<ProdutoDTO>> Get()
    {
        var produtos = _iof.ProdutoRepository.GetAll();
        if (produtos is null) { return NotFound(); }
        // var destino = _mapper.Map<Destino>(origem);
        var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
        return Ok(produtosDto);
    }

    [HttpGet("{id}", Name = "ObterProduto")]
    public ActionResult<ProdutoDTO> Get(int id)
    {
        var produto = _iof.ProdutoRepository.Get(p => p.ProdutoId == id);
        if (produto is null)
        {
            _logger.LogWarning($"Produto com id = {id} não encontrado...");
            return NotFound($"Produto com id = {id} não encontrado...");
        }
        var produtoDto = _mapper.Map<ProdutoDTO>(produto);
        return Ok(produtoDto);
    }

    [HttpPost]
    public ActionResult Create(ProdutoDTO produtoDto)
    {
        if (produtoDto is null)
            return BadRequest();

        var produto = _mapper.Map<Produto>(produtoDto);

        var novoProduto = _iof.ProdutoRepository.Create(produto);
        _iof.Commit();

        var novoProdutoDto = _mapper.Map<ProdutoDTO>(novoProduto);

        return new CreatedAtRouteResult("ObterProduto", new { id = novoProdutoDto.ProdutoId }, novoProdutoDto);
    }

    [HttpPut("{id:int}")]
    public ActionResult<ProdutoDTO> Update(int id, ProdutoDTO produtoDto)
    {
        if (id != produtoDto.ProdutoId)
        {
            return BadRequest();
        }
        var produto =_mapper.Map<Produto>(produtoDto);
        var produtoAtualizado = _iof.ProdutoRepository.Update(produto);
        _iof.Commit();
        var produtoAtualizadoDto = _mapper.Map<ProdutoDTO>(produtoAtualizado);
        return Ok(produtoAtualizadoDto);
    }

    [HttpDelete("{id:int}")]
    public ActionResult<ProdutoDTO> Delete(int id)
    {
        var produto = _iof.ProdutoRepository.Get(p => p.ProdutoId == id);
        if (produto is null) { return NotFound("Produto não encontrado."); }
        var produtoDeletado = _iof.ProdutoRepository.Delete(produto);
        _iof.Commit();
        var produtoDeletadoDto = _mapper.Map<ProdutoDTO>(produtoDeletado);
        return Ok(produtoDeletadoDto);
    }
}
