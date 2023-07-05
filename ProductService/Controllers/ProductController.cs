using Microsoft.AspNetCore.Mvc;

namespace ProductService.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class ProductController
{
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "Windows Phone", "BlackBerry", "iPhone" };
    }
}