namespace APICatalogo.Repositories.IRepositories
{
    public interface IUnitOfWork
    {
        IProdutoRepository produtoRepository { get; }
        ICategoriaRepository categoriaRepository { get; }
        void Commit();
    }
}
