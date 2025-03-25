using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly IUnitOfWork _iof;
    private readonly ILogger<CategoriasController> _logger;

    public CategoriasController(IUnitOfWork iof, ILogger<CategoriasController> logger)
    {
        _iof = iof;
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Categoria>> Get()
    {
        var categorias = _iof.categoriaRepository.GetAll();

        var CategoriaDTO = new List<CategoriaDTO>();
        foreach (var item in categorias)
        {
            var categoriaDto = new CategoriaDTO()
            {
                CategoriaId = item.CategoriaId,
                Nome = item.Nome,
                ImageUrl = item.ImageUrl
            };
            CategoriaDTO.Add(categoriaDto);
        }

        return Ok(CategoriaDTO);
    }

    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public ActionResult<CategoriaDTO> Get(int id)
    {
        var categoria = _iof.categoriaRepository.Get(c => c.CategoriaId == id);

        if (categoria is null)
        {
            _logger.LogWarning($"Categoria com id = {id} não encontrada...");
            return NotFound($"Categoria com id = {id} não encontrada...");
        }

        var categoriaDTO = new CategoriaDTO()
        {
            CategoriaId = categoria.CategoriaId,
            Nome = categoria.Nome,
            ImageUrl = categoria.ImageUrl
        };

        return Ok(categoriaDTO);
    }

    [HttpPost]
    public ActionResult<CategoriaDTO> Post(CategoriaDTO categoriaDto)
    {
        if (categoriaDto is null)
        {
            _logger.LogWarning($"Dados inválidos...");
            return BadRequest("Dados inválidos");
        }

        var categoria = new Categoria()
        {
            CategoriaId = categoriaDto.CategoriaId,
            Nome = categoriaDto.Nome,
            ImageUrl = categoriaDto.ImageUrl
        };

        var categoriaCriada = _iof.categoriaRepository.Create(categoria);
        _iof.Commit();

        var categoriaDTO = new CategoriaDTO()
        {
            CategoriaId = categoriaCriada.CategoriaId,
            Nome = categoriaCriada.Nome,
            ImageUrl = categoriaCriada.ImageUrl
        };

        return new CreatedAtRouteResult("ObterCategoria", new { id = categoriaCriada.CategoriaId }, categoriaDto);
    }

    [HttpPut("{id:int}")]
    public ActionResult<CategoriaDTO> Put(int id, CategoriaDTO categoriaDto)
    {
        if (id != categoriaDto.CategoriaId)
        {
            _logger.LogWarning($"Dados inválidos...");
            return BadRequest("Dados inválidos");
        }

        var categoria = new Categoria()
        {
            CategoriaId = categoriaDto.CategoriaId,
            Nome = categoriaDto.Nome,
            ImageUrl = categoriaDto.ImageUrl
        };

        _iof.categoriaRepository.Update(categoria);
        _iof.Commit();

        var categoriaAtualizadaDTO = new CategoriaDTO()
        {
            CategoriaId = categoria.CategoriaId,
            Nome = categoria.Nome,
            ImageUrl = categoria.ImageUrl
        };

        return Ok(categoriaAtualizadaDTO);
    }

    [HttpDelete("{id:int}")]
    public ActionResult<CategoriaDTO> Delete(int id)
    {
        var categoria = _iof.categoriaRepository.Get(c => c.CategoriaId == id);

        if (categoria == null)
        {
            _logger.LogWarning($"Categoria com id = {id} não encontrada...");
            return NotFound($"Categoria com id = {id} não encontrada...");
        }

        var categoriaExcluida = _iof.categoriaRepository.Delete(categoria);
        _iof.Commit();

        var categoriaExcluidaDTO = new CategoriaDTO()
        {
            CategoriaId = categoria.CategoriaId,
            Nome = categoria.Nome,
            ImageUrl = categoria.ImageUrl
        };
        return Ok(categoriaExcluidaDTO);
    }
}
