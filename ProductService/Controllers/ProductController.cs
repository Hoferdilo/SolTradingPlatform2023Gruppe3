using Consul;
using Microsoft.AspNetCore.Mvc;

namespace ProductService.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class ProductController
{
    public ProductController(IConsulClient consulClient)
    {
        _consulClient = consulClient;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery]Guid auth)
    {
        var key = await _consulClient.KV.Get("productAccessKey");
        var id = new Guid(key.Response.Value);
        if (id != auth)
        {
            return new StatusCodeResult(401);
        }
        return new OkObjectResult(new string[] { "Windows Phone", "BlackBerry", "iPhone" });
    }

    private readonly IConsulClient _consulClient;
}