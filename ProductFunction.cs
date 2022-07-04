using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using EAuction.Products.Api.Repositories;
using EAuction.Products.Api.Repositories.Abstractions;

namespace EAuction.Products.AzureFunction.API
{
    public class ProductFunction
    {
        private readonly IProductRepository _productRepository;
        public ProductFunction(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [FunctionName("DeleteProduct")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "DeleteProduct/{productId}")] HttpRequest req,string productId,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var result = await _productRepository.Delete(productId);
            return new OkObjectResult(result);
        }
    }
}
