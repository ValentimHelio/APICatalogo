using System.ComponentModel.DataAnnotations;

namespace APICatalogo.DTOs
{
    public class ProdutoDTO
    {
        public int ProdutoId { get; set; }

        [Required]
        [StringLength(100)]
        public string? Nome { get; set; } = string.Empty;

        [Required]
        [StringLength(400)]
        public string? Descricao { get; set; } = string.Empty;

        [Required]
        public decimal Preco { get; set; }

        [Required]
        [StringLength(400)]
        public string? ImageUrl { get; set; }

        public int CategoriaId { get; set; }

    }
}
