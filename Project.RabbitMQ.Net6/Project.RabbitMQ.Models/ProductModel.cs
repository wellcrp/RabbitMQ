namespace Project.RabbitMQ.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = default!;
        public string ProductDescription { get; set; } = default!;
        public int ProductPrice { get; set; }
        public int ProductStock { get; set; }        
    }
}
