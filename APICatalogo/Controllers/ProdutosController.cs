using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repositories.IRepositories;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;
    private readonly ILogger<ProdutosController> _logger;

    public ProdutosController(IUnitOfWork iof, IMapper mapper, ILogger<ProdutosController> logger)
    {
        _uof = iof;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet("produtoPorCategoria/{id}")]
    public ActionResult<IEnumerable<ProdutoDTO>> GetProdutosPorCategoria(int id)
    {
        var produtos = _uof.ProdutoRepository.GetProdutosPorCategoria(id);
        if (produtos.Count().Equals(0))
        {
            _logger.LogWarning($"Produto com a CatergoriaId = {id} não encontrado...");
            return NotFound($"Produto com a CatergoriaId = {id} não encontrado...");
        }
        // var destino = _mapper.Map<Destino>(origem);
        var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
        return Ok(produtosDto);
    }

    [HttpGet("pagination")]
    public ActionResult<IEnumerable<ProdutoDTO>> Get([FromQuery] ProdutoParameters produtoParameters)
    {
        var produtos = _uof.ProdutoRepository.GetProdutos(produtoParameters);

        var metadados = new
        {
            produtos.TotalCount,
            produtos.PageSize,
            produtos.CurrentPage,
            produtos.TotalPages,
            produtos.HasNext,
            produtos.HasPrevious
        };

        Response.Headers.Append("X-Pagitation", JsonConvert.SerializeObject(metadados));

        var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

        return Ok(produtosDto);
    }

    [HttpGet]
    public ActionResult<IEnumerable<ProdutoDTO>> Get()
    {
        var produtos = _uof.ProdutoRepository.GetAll();
        if (produtos is null) { return NotFound(); }
        // var destino = _mapper.Map<Destino>(origem);
        var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
        return Ok(produtosDto);
    }

    [HttpGet("{id}", Name = "ObterProduto")]
    public ActionResult<ProdutoDTO> Get(int id)
    {
        var produto = _uof.ProdutoRepository.Get(p => p.ProdutoId == id);
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

        var novoProduto = _uof.ProdutoRepository.Create(produto);
        _uof.Commit();

        var novoProdutoDto = _mapper.Map<ProdutoDTO>(novoProduto);

        return new CreatedAtRouteResult("ObterProduto", new { id = novoProdutoDto.ProdutoId }, novoProdutoDto);
    }

    [HttpPatch("{id}/UpdatePartial")]
    public ActionResult<ProdutoDTOUpdateResponse> Patch(int id, JsonPatchDocument<ProdutoDTOUpdateRequest> patchProdutoDto)
    {
        //valida input 
        if (patchProdutoDto == null || id <= 0)
            return BadRequest();

        //obtem o produto pelo Id
        var produto = _uof.ProdutoRepository.Get(c => c.ProdutoId == id);

        //se não econtrou retorna
        if (produto == null)
            return NotFound();

        //mapeia produto para ProdutoDTOUpdateRequest
        var produtoUpdateRequest = _mapper.Map<ProdutoDTOUpdateRequest>(produto);

        //aplica as alterações definidas no documento JSON Patch ao objeto ProdutoDTOUpdateRequest
        patchProdutoDto.ApplyTo(produtoUpdateRequest, ModelState);

        if (!ModelState.IsValid || !TryValidateModel(produtoUpdateRequest))
            return BadRequest(ModelState);

        // Mapeia as alterações de volta para a entidade Produto
        _mapper.Map(produtoUpdateRequest, produto);

        // Atualiza a entidade no repositório
        _uof.ProdutoRepository.Update(produto);
        // Salve as alterações no banco de dados
        _uof.Commit();

        //retorna ProdutoDTOUpdateResponse
        return Ok(_mapper.Map<ProdutoDTOUpdateResponse>(produto));
    }

    [HttpPut("{id:int}")]
    public ActionResult<ProdutoDTO> Update(int id, ProdutoDTO produtoDto)
    {
        if (id != produtoDto.ProdutoId)
        {
            return BadRequest();
        }
        var produto =_mapper.Map<Produto>(produtoDto);
        var produtoAtualizado = _uof.ProdutoRepository.Update(produto);
        _uof.Commit();
        var produtoAtualizadoDto = _mapper.Map<ProdutoDTO>(produtoAtualizado);
        return Ok(produtoAtualizadoDto);
    }

    [HttpDelete("{id:int}")]
    public ActionResult<ProdutoDTO> Delete(int id)
    {
        var produto = _uof.ProdutoRepository.Get(p => p.ProdutoId == id);
        if (produto is null) { return NotFound("Produto não encontrado."); }
        var produtoDeletado = _uof.ProdutoRepository.Delete(produto);
        _uof.Commit();
        var produtoDeletadoDto = _mapper.Map<ProdutoDTO>(produtoDeletado);
        return Ok(produtoDeletadoDto);
    }
}
