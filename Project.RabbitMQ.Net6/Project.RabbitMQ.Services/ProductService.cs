using Project.RabbitMQ.Infrastructure;
using Project.RabbitMQ.Models;
using Project.RabbitMQ.Services.Interface;

namespace Project.RabbitMQ.Services
{
    public class ProductService : IProductService
    {
        private readonly DbContextData _dbContext;
        public ProductService(DbContextData dbContext)
        {
            dbContext = dbContext;
        }

        public ProductModel AddProduct(ProductModel product)
        {
            var result = _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
            return result.Entity;
        }

        public bool DeleteProduct(int Id)
        {
            var filteredData = _dbContext.Products.Where(x => x.ProductId == Id).FirstOrDefault();
            var result = _dbContext.Remove(filteredData);
            _dbContext.SaveChanges();
            return result != null ? true : false;
        }

        public ProductModel GetProductById(int id)
        {
            return _dbContext.Products.FirstOrDefault(x => x.ProductId == id);
        }

        public IEnumerable<ProductModel> GetProductList()
        {
            return _dbContext.Products.ToList();
        }

        public ProductModel UpdateProduct(ProductModel product)
        {
            var result = _dbContext.Products.Update(product);
            _dbContext.SaveChanges();
            return result.Entity;
        }
    }
}
