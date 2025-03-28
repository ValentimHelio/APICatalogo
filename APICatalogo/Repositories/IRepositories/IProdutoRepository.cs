using APICatalogo.Models;
using APICatalogo.Pagination;

namespace APICatalogo.Repositories.IRepositories;

public interface IProdutoRepository : IRepository<Produto>
{
    //IEnumerable<Produto> GetProdutos(ProdutoParameters produtoParameters);
    Task<PagedList<Produto>> GetProdutosAsync(ProdutoParameters produtoParameters);
    Task<PagedList<Produto>> GetProdutosFiltroPrecoAsync(ProdutosFiltroPreco produtoFiltroPreco);
    Task<IEnumerable<Produto>> GetProdutosPorCategoriaAsync(int id);
}
