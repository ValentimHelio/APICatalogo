using APICatalogo.Models;
using APICatalogo.Servives.IServives;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManage;
        private readonly IConfiguration _configuration;

        public AuthController(ITokenService tokenService, UserManager<ApplicationUser> userManager,
                                    RoleManager<IdentityRole> roleManage, IConfiguration configuration)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _roleManage = roleManage;
            _configuration = configuration;
        }
    }
}
