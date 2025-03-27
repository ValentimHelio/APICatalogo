using APICatalogo.Models;
using APICatalogo.Pagination;

namespace APICatalogo.Repositories.IRepositories;

public interface IProdutoRepository : IRepository<Produto>
{
    IEnumerable<Produto> GetProdutos(ProdutoParameters produtoParameters);
    IEnumerable<Produto> GetProdutosPorCategoria(int id);
}
