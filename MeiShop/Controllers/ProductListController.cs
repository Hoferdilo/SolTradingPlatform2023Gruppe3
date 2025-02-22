﻿using Consul;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace MeiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductListController : ControllerBase
    {
        public ProductListController(IConfiguration configuration, IHttpClientFactory clientFactory, IConsulClient consulClient)
        {
            _configuration = configuration;
            _clientFactory = clientFactory;
            _consulClient = consulClient;
        }

        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            var resultList = new List<string>();
            var resultServices = await _consulClient.Agent.Services();
            var key = await _consulClient.KV.Get("productAccessKey");
            var id = new Guid(key.Response.Value);
            var productApis = resultServices.Response.Values.Where(x => x.Service == "ProductService")
                .Select(x => x.Address);
            var client = _clientFactory.CreateClient();
            foreach (var productApi in productApis)
            {
                try
                {
                    var result = await client.GetAsync($"http://{productApi}/api/Product?auth={id}");
                    var content = await result.Content.ReadFromJsonAsync<string[]>();
                    if (content != null)
                    {
                        resultList.AddRange((content).ToList());
                    }
                }
                catch (Exception ex)
                {
                    Log.Warning($"Could not connect to {productApi}", ex);
                }
            }

            return resultList;
        }

        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConsulClient _consulClient;

    }
}
