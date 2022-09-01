using Project.RabbitMQ.Models;

namespace Project.RabbitMQ.Services.Interface
{
    public interface IProductService
    {
        public IEnumerable<ProductModel> GetProductList();
        public ProductModel GetProductById(int id);
        public ProductModel AddProduct(ProductModel product);
        public ProductModel UpdateProduct(ProductModel product);
        public bool DeleteProduct(int Id);
    }
}
