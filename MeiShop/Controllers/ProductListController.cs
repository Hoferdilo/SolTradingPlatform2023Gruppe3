using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductListController : ControllerBase
    {
        public ProductListController(IConfiguration configuration, IHttpClientFactory clientFactory)
        {
            _configuration = configuration;
            _clientFactory = clientFactory;
        }

        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            var resultList = new List<string>();
            var productApis = _configuration.GetSection("ProductApis").Get<string[]>();
            var client = _clientFactory.CreateClient();
            foreach (var productApi in productApis)
            {
                var result = await client.GetAsync($"http://{productApi}");
                var content = await result.Content.ReadFromJsonAsync<string[]>();
                if (content != null)
                {
                    resultList.AddRange((content).ToList());
                }
            }

            return resultList;
        }

        private IConfiguration _configuration;
        private IHttpClientFactory _clientFactory;

    }
}
