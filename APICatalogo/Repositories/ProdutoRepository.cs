using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repositories.IRepositories;

namespace APICatalogo.Repositories
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext context) : base(context) { }

        //public IEnumerable<Produto> GetProdutos(ProdutoParameters produtoParameters)
        //{
        //    return GetAll()
        //        .OrderBy(p => p.Nome)
        //        .Skip((produtoParameters.PageNumber - 1) * produtoParameters.PageSize)
        //        .Take(produtoParameters.PageSize).ToList();
        //}

        PagedList<Produto> IProdutoRepository.GetProdutos(ProdutoParameters produtoParameters)
        {
            var produtos = GetAll().OrderBy(p => p.ProdutoId).AsQueryable();
            var produtosOrdenados = PagedList<Produto>.ToPagedList(produtos, produtoParameters.PageNumber, produtoParameters.PageSize);
            return produtosOrdenados;
        }

        public IEnumerable<Produto> GetProdutosPorCategoria(int id)
        {
            return GetAll().Where(c => c.CategoriaId == id);
        }

    }
}
