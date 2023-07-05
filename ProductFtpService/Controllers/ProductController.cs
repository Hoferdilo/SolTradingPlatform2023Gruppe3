using Consul;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace ProductFtpService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController
{
    public ProductController(IConfiguration configuration, IConsulClient consulClient)
    {
        _configuration = configuration;
        _consulClient = consulClient;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetProducts([FromQuery] Guid auth)
    {
        var key = await _consulClient.KV.Get("productAccessKey");
        var id = new Guid(key.Response.Value);
        if (id != auth)
        {
            return new StatusCodeResult(401);
        }

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

        return new OkObjectResult(_productDatabase);
    }

    private readonly IConsulClient _consulClient;
    private readonly IConfiguration _configuration;
    private static List<string>? _productDatabase;

}