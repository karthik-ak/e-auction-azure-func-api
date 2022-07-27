using EAuction.Products.Api.Repositories.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

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
