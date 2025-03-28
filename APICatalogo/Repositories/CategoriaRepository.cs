using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repositories.IRepositories;
using X.PagedList;

namespace APICatalogo.Repositories;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbContext context) : base(context) { }

    public async Task<IPagedList<Categoria>> GetCategoriasAsync(CategoriaParameters categoriasParameters)
    {
        var categorias = await GetAllAsync();
        var categoriasOrdenados = categorias.OrderBy(p => p.CategoriaId).AsQueryable();
  
        //var resultado = PagedList<Categoria>.ToPagedList(categoriasOrdenados, categoriaParameters.PageNumber, categoriaParameters.PageSize);
        var resultado = await categoriasOrdenados.ToPagedListAsync(categoriasParameters.PageNumber, categoriasParameters.PageSize);
        return resultado;
    }

    public async Task<IPagedList<Categoria>> GetCategoriasFiltroNomeAsync(CategoriaFiltroNome categoriasParams)
    {
        var categorias = await GetAllAsync();

        if (!string.IsNullOrEmpty(categoriasParams.Nome))
        {
            categorias = categorias.Where(c => c.Nome.Contains(categoriasParams.Nome, StringComparison.OrdinalIgnoreCase));
        }

        //var categoriasFiltradas = PagedList<Categoria>.ToPagedList(categorias.AsQueryable(), categoriasParams.PageNumber, categoriasParams.PageSize);
        var categoriasFiltradas = await categorias.ToPagedListAsync(categoriasParams.PageNumber, categoriasParams.PageSize);
        return categoriasFiltradas;
    }

}
