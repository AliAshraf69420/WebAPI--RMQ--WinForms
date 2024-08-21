using Microsoft.AspNetCore.Mvc;
using WebApi_rabbitmq.Models;
using WebApi_rabbitmq.RabbitMQ;
using WebApi_rabbitmq.Services;
namespace WebApi_rabbitmq.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductService ProductService;
        private readonly IRabbitMQProducer _rabitMQProducer;
        public ProductController(IProductService _productService, IRabbitMQProducer rabitMQProducer)
        {
            ProductService = _productService;
            _rabitMQProducer = rabitMQProducer;
        }
        [HttpGet("GetProductByID")]
        public ProductClass GetProductById(int Id)
        {
            return ProductService.GetProductByID(Id);
        }
        [HttpPost("AddProduct")]
        public ProductClass AddProduct(ProductClass product_)
        {
            var productData = ProductService.AddProduct(product_);
            _rabitMQProducer.SendProductMessage(productData);
            return productData;
        }
        [HttpPut("UpdateProduct")]
        public ProductClass UpdateProduct(ProductClass product_)
        {
            return ProductService.UpdateProduct(product_);
        }
        [HttpDelete("DeleteProduct")]
        public bool DeleteProduct(int Id)
        {
            return ProductService.DeleteProduct(Id);
        }
    }
}
