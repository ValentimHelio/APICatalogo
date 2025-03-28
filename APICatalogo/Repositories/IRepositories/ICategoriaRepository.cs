using APICatalogo.Models;
using APICatalogo.Pagination;

namespace APICatalogo.Repositories.IRepositories;

public interface ICategoriaRepository : IRepository<Categoria>
{
    Task<PagedList<Categoria>> GetCategoriasAsync(CategoriaParameters categoriaParameters);
    Task<PagedList<Categoria>> GetCategoriasFiltroNomeAsync(CategoriaFiltroNome categoriaParameters);
}
