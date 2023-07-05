using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace ProductFtpService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController
{
    public ProductController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    [HttpGet]
    public async Task<List<string>?> GetProducts()
    {
        if (_productDatabase == null)
        {
            _productDatabase = new List<string>();
            var stream = File.OpenRead(_configuration["ProductList"]);
            using var reader = new StreamReader(stream);
            //skip header
            await reader.ReadLineAsync();
            string? line = null;
            while((line = await reader.ReadLineAsync()) != null)
            {
                _productDatabase.Add(line);
            }
        }

        return _productDatabase;
    }

    private readonly IConfiguration _configuration;
    private static List<string>? _productDatabase;

}