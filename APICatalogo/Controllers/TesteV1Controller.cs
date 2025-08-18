using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers;

[Route("api/v{versio:apiVersion}/teste")]
[ApiController]
//[ApiVersion("1.0")]
[ApiVersion("1.0", Deprecated = true)]
public class TesteV1Controller : ControllerBase
{
    [HttpGet]
    public string GetVersion()
    {
        return "TesteV1 - GET - API Versão 1.0";
    }
}
