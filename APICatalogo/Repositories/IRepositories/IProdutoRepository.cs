using APICatalogo.Models;
using APICatalogo.Pagination;

namespace APICatalogo.Repositories.IRepositories;

public interface IProdutoRepository : IRepository<Produto>
{
    //IEnumerable<Produto> GetProdutos(ProdutoParameters produtoParameters);
    PagedList<Produto> GetProdutos(ProdutoParameters produtoParameters);
    PagedList<Produto> GetProdutosFiltroPreco(ProdutosFiltroPreco produtoFiltroPreco);
    IEnumerable<Produto> GetProdutosPorCategoria(int id);
}
