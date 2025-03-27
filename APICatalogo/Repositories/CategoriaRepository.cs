using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repositories.IRepositories;

namespace APICatalogo.Repositories;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbContext context) : base(context) { }

    public PagedList<Categoria> GetCategorias(CategoriaParameters categoriaParameters)
    {
        var categorias = GetAll().OrderBy(p => p.CategoriaId).AsQueryable();
        var categoriasOrdenados = PagedList<Categoria>.ToPagedList(categorias, categoriaParameters.PageNumber, categoriaParameters.PageSize);
        return categoriasOrdenados;
    }
}
