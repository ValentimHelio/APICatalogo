namespace APICatalogo.Models
{
    public class Produto
    {
        public int ProdutoId { get; set; }
        public string? Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public string? ImageUrl { get; set; }
        public float Estoque { get; set; }
        public DateTime DataCadastro { get; set; }

    }
}
