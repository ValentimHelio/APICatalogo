using APICatalogo.Context;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        public readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }
    }
}
