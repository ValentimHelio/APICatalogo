using APICatalogo.Models;

namespace APICatalogo.DTOs.Mappings
{
    public static class CategoriaDTOMappingExtensions
    {
        public static CategoriaDTO? ToCategoriaDTO(Categoria categoria)
        {
            if (categoria is null) { return null; }
            return new CategoriaDTO()
            {
                CategoriaId = categoria.CategoriaId,
                Nome = categoria.Nome,
                ImageUrl = categoria.ImageUrl
            };
        }

        public static Categoria? ToCategoria(CategoriaDTO categoriaDto)
        {
            if (categoriaDto is null) { return null; }
            return new Categoria()
            {
                CategoriaId = categoriaDto.CategoriaId,
                Nome = categoriaDto.Nome,
                ImageUrl = categoriaDto.ImageUrl
            };
        }

        public static IEnumerable<CategoriaDTO> ToCategoriaDTOList(IEnumerable<Categoria> categorias)
        {
            if (categorias is null || !categorias.Any()) { return new List<CategoriaDTO>(); }
            return categorias.Select(c => new CategoriaDTO
            {
                CategoriaId = c.CategoriaId,
                Nome = c.Nome,
                ImageUrl = c.ImageUrl
            }).ToList();
        }
    }
}
