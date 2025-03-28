using APICatalogo.Models;
using APICatalogo.Pagination;
using X.PagedList;

namespace APICatalogo.Repositories.IRepositories;

public interface IProdutoRepository : IRepository<Produto>
{
    //IEnumerable<Produto> GetProdutos(ProdutoParameters produtoParameters);
    Task<IPagedList<Produto>> GetProdutosAsync(ProdutoParameters produtoParameters);
    Task<IPagedList<Produto>> GetProdutosFiltroPrecoAsync(ProdutosFiltroPreco produtoFiltroPreco);
    Task<IEnumerable<Produto>> GetProdutosPorCategoriaAsync(int id);
}
