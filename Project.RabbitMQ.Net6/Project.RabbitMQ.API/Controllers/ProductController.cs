using Microsoft.AspNetCore.Mvc;
using Project.RabbitMQ.Infrastructure.Interface;
using Project.RabbitMQ.Models;
using Project.RabbitMQ.Services.Interface;

namespace Project.RabbitMQ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService _productService;
        private readonly IRabbitMQProducer _rabbitMQProducer;
        public ProductController(IProductService productService, IRabbitMQProducer rabbitMQProducer)
        {
            _productService = productService;
            _rabbitMQProducer = rabbitMQProducer;
        }

        [HttpGet("productlist")]
        public IEnumerable<ProductModel> ProductList()
        {
            var productList = _productService.GetProductList();
            return productList;
        }
        [HttpGet("getproductbyid")]
        public ProductModel GetProductById(int Id)
        {
            return _productService.GetProductById(Id);
        }
        [HttpPost("addproduct")]
        public ProductModel AddProduct(ProductModel product)
        {
            var productData = _productService.AddProduct(product);
            //send the inserted product data to the queue and consumer will listening this data from queue
            _rabbitMQProducer.SendProductDirectExchange(productData);
            return productData;
        }
        [HttpPut("updateproduct")]
        public ProductModel UpdateProduct(ProductModel product)
        {
            return _productService.UpdateProduct(product);
        }
        [HttpDelete("deleteproduct")]
        public bool DeleteProduct(int Id)
        {
            return _productService.DeleteProduct(Id);
        }
    }
}
}
