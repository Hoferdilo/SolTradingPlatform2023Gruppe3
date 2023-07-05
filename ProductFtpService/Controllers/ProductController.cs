using Microsoft.AspNetCore.Mvc;

namespace ProductFtpService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController
{
    [HttpGet]
    public async Task<string[]> GetProducts()
    {
        
        return new[] {""};
    }
}