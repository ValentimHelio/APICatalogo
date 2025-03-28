using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repositories.IRepositories;

namespace APICatalogo.Repositories;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbContext context) : base(context) { }

    public async Task<PagedList<Categoria>> GetCategoriasAsync(CategoriaParameters categoriaParameters)
    {
        var categorias = await GetAllAsync();
        var categoriasOrdenados = categorias.OrderBy(p => p.CategoriaId).AsQueryable();
  
        var resultado = PagedList<Categoria>.ToPagedList(categoriasOrdenados, categoriaParameters.PageNumber, categoriaParameters.PageSize);
        return resultado;
    }

    public async Task<PagedList<Categoria>> GetCategoriasFiltroNomeAsync(CategoriaFiltroNome categoriasParams)
    {
        var categorias = await GetAllAsync();

        if (!string.IsNullOrEmpty(categoriasParams.Nome))
        {
            categorias = categorias.Where(c => c.Nome.Contains(categoriasParams.Nome, StringComparison.OrdinalIgnoreCase));
        }

        var categoriasFiltradas = PagedList<Categoria>.ToPagedList(categorias.AsQueryable(), categoriasParams.PageNumber, categoriasParams.PageSize);

        return categoriasFiltradas;
    }

}
